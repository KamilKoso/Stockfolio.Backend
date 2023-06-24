using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stockfolio.Modules.Assets.Application.Dto.Assets;
using Stockfolio.Modules.Assets.Application.Dto.Forex;
using Stockfolio.Modules.Assets.Application.Repositories;
using Stockfolio.Modules.Assets.Infrastructure.Exceptions;
using Stockfolio.Modules.Assets.Infrastructure.YahooFinance.Models;
using Stockfolio.Modules.Assets.Infrastructure.YahooFinance.Models.HistoricalData;
using Stockfolio.Modules.Assets.Infrastructure.YahooFinance.Options;
using Stockfolio.Shared.Abstractions.Kernel.ValueObjects.Currencies;
using Stockfolio.Shared.Infrastructure.Serialization;
using System.Net;
using System.Text.Json.Nodes;

namespace Stockfolio.Modules.Assets.Infrastructure.YahooFinance.Repositories;

internal class YahooFinanceApi : IStockMarketRepository, IForexRepository
{
    private readonly HttpClient _httpClient;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly YahooFinanceOptions _options;
    private readonly ILogger<YahooFinanceApi> _logger;
    private readonly string quoteEvents = string.Join('|', QuoteEvent.CapitalGains, QuoteEvent.Dividends, QuoteEvent.Splits);
    internal static string _crumb;

    public YahooFinanceApi(HttpClient httpClient,
                           IJsonSerializer jsonSerializer,
                           IOptions<YahooFinanceOptions> options,
                           ILogger<YahooFinanceApi> logger)
    {
        _httpClient = httpClient;
        _jsonSerializer = jsonSerializer;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<SearchAssetsResultDto> SearchSecurities(string searchQuery, int quotesCount = 7, CancellationToken cancellationToken = default)
    {
        var requestUrl = $"v1/finance/search?q={searchQuery}&quotesCount={quotesCount}&newsCount=0&listCount=0";
        var response = await _httpClient.GetAsync(requestUrl, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new FinancialDataProviderError(response.RequestMessage.RequestUri.ToString(),
                                                 response.StatusCode,
                                                 await response.Content.ReadAsStringAsync());
        }

        var responseStr = await response.Content.ReadAsStringAsync(cancellationToken);
        var quotesJsonArray = JsonNode.Parse(responseStr)["quotes"]?.ToString();
        var searchQuoteDto = _jsonSerializer.Deserialize<IEnumerable<SearchYahooFinanceSecurities>>(quotesJsonArray).ToDictionary(x => x.Symbol);
        var quoteDetails = (await GetQuotesBySymbols(searchQuoteDto.Keys, cancellationToken))
                                            .Select(x => x.AsSearchQuoteDto(searchQuoteDto[x.Symbol]))
                                            .ToList();

        return new(quoteDetails);
    }

    public async Task<IEnumerable<AssetDto>> GetSecurities(IEnumerable<string> symbols, CancellationToken cancellationToken = default)
    {
        if (!symbols.Any())
            return new List<AssetDto>();

        return (await GetQuotesBySymbols(symbols, cancellationToken))
                        .Select(x => x.AsQuoteDetailsDto());
    }

    public async Task<AssetDto> GetSecurity(string symbol, CancellationToken cancellationToken = default)
    {
        return (await GetSecurities(new string[] { symbol }, cancellationToken)).FirstOrDefault();
    }

    public async Task<HistoricalQuotesDto> GetHistoricalQuotes(string symbol,
                                                           DateTimeOffset start,
                                                           DateTimeOffset end,
                                                           string interval = "1d",
                                                           CancellationToken cancellationToken = default)
    {
        var requestUrl = $"v8/finance/chart/{symbol}?period1={start.ToUnixTimeSeconds()}&period2={end.ToUnixTimeSeconds()}&events={quoteEvents}&interval={interval}";
        return (await GetHistoricalDataByUri(requestUrl, cancellationToken)).AsQuoteHistoricalData();
    }

    public async Task<HistoricalQuotesDto> GetHistoricalQuotes(string symbol,
                                                           string range,
                                                           string interval = "1d",
                                                           CancellationToken cancellationToken = default)
    {
        var requestUrl = $"v8/finance/chart/{symbol}?range={range}&events={quoteEvents}&interval={interval}";
        return (await GetHistoricalDataByUri(requestUrl, cancellationToken)).AsQuoteHistoricalData();
    }

    public async Task<ExchangeRatesDto> GetHistoricalExchangeRates(Currency from, Currency to, DateTimeOffset start, DateTimeOffset end, CancellationToken cancellationToken = default)
    {
        var result = await GetHistoricalQuotes(ConvertCurrencyPairToSymbol(from, to), start, end, cancellationToken: cancellationToken);
        var exchangeRates = result.Quotes
            .Where(x => x.Value is not null)
            .Select(x => new ExchangeRateDto((decimal)x.Value, x.Date));
        return new(from, to, exchangeRates);
    }

    internal async Task RefreshTheSession()
    {
        _logger.LogTrace("YahooFinance session refresh initialized.");
        // This request will obtain "A3" cookie which is required for obtaining the crumb
        var cookieResponse = await _httpClient.GetAsync(_options.UrlToObtainTheCookie);

        // It is expected to get 404 here, cookie will be issued anyway
        if (cookieResponse.StatusCode is not HttpStatusCode.NotFound)
        {
            throw new FinancialDataProviderError(cookieResponse.RequestMessage.RequestUri.ToString(),
                                                 cookieResponse.StatusCode,
                                                 await cookieResponse.Content.ReadAsStringAsync());
        }

        var crumbResponse = await _httpClient.GetAsync("/v1/test/getcrumb");

        if (!crumbResponse.IsSuccessStatusCode)
        {
            throw new FinancialDataProviderError(crumbResponse.RequestMessage.RequestUri.ToString(),
                                                 crumbResponse.StatusCode,
                                                 await crumbResponse.Content.ReadAsStringAsync());
        }

        _crumb = await crumbResponse.Content.ReadAsStringAsync();
        _logger.LogTrace("Refreshing YahooFinance session completed");
    }

    private async Task<YahooFinanceQuoteHistoricalData> GetHistoricalDataByUri(string uri, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(uri, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new FinancialDataProviderError(response.RequestMessage.RequestUri.ToString(),
                                                 response.StatusCode,
                                                 await response.Content.ReadAsStringAsync());
        }

        var responseStr = await response.Content.ReadAsStringAsync(cancellationToken);
        var contentStr = JsonNode.Parse(responseStr)["chart"]["result"][0].ToString();
        return _jsonSerializer.Deserialize<YahooFinanceQuoteHistoricalData>(contentStr);
    }

    private async Task<IEnumerable<YahooFinanceSecurityDetails>> GetQuotesBySymbols(IEnumerable<string> symbols, CancellationToken cancellationToken = default)
    {
        if (!symbols.Any())
            return new List<YahooFinanceSecurityDetails>();

        var requestUrl = $"v7/finance/quote?symbols={string.Join(",", symbols)}&crumb={_crumb}";
        var response = await _httpClient.GetAsync(requestUrl, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new FinancialDataProviderError(response.RequestMessage.RequestUri.ToString(),
                                                 response.StatusCode,
                                                 await response.Content.ReadAsStringAsync());
        }

        var responseStr = await response.Content.ReadAsStringAsync(cancellationToken);
        var quotesJsonArray = JsonNode.Parse(responseStr)["quoteResponse"]["result"].ToString();
        return _jsonSerializer.Deserialize<IEnumerable<YahooFinanceSecurityDetails>>(quotesJsonArray);
    }

    private static string ConvertCurrencyPairToSymbol(Currency from, Currency to)
        => $"{from.Code}{to.Code}=X";
}