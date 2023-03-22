using Stockfolio.Modules.Portfolios.Application.Models;
using Stockfolio.Modules.StockMarket.Application.Dto;
using Stockfolio.Modules.StockMarket.Application.DTO;
using Stockfolio.Modules.StockMarket.Application.Repositories;
using Stockfolio.Modules.StockMarket.Infrastructure.YahooFinance.Models;
using Stockfolio.Shared.Infrastructure.Serialization;
using System.Text.Json.Nodes;

namespace Stockfolio.Modules.StockMarket.Infrastructure.YahooFinance.Repositories;

internal class YahooFinanceApi : IQuotesRepository
{
    private readonly HttpClient _httpClient;
    private readonly IJsonSerializer _jsonSerializer;

    public YahooFinanceApi(HttpClient httpClient,
                           IJsonSerializer jsonSerializer)
    {
        _httpClient = httpClient;
        _jsonSerializer = jsonSerializer;
    }

    public async Task<SearchQuotesDto> SearchQuotes(string searchQuery, int quotesCount = 7, CancellationToken cancellationToken = default)
    {
        var requestUrl = $"v1/finance/search?q={searchQuery}&quotesCount={quotesCount}&newsCount=0&listCount=0";
        var response = await _httpClient.GetAsync(requestUrl, cancellationToken);
        var responseStr = await response.Content.ReadAsStringAsync(cancellationToken);
        var quotesJsonArray = JsonNode.Parse(responseStr)["quotes"]?.ToString();
        var searchQuoteDto = _jsonSerializer.Deserialize<IEnumerable<SearchYahooFinanceQuote>>(quotesJsonArray).ToDictionary(x => x.Symbol);
        var quoteDetails = (await GetQuotesBySymbols(searchQuoteDto.Keys, cancellationToken))
                                            .Select(x => x.AsSearchQuoteDto(searchQuoteDto[x.Symbol]))
                                            .ToList();

        return new(quoteDetails);
    }

    public async Task<IEnumerable<QuoteDetailsDto>> GetQuotes(IEnumerable<string> symbols, CancellationToken cancellationToken = default)
    {
        if (!symbols.Any())
            return new List<QuoteDetailsDto>();

        return (await GetQuotesBySymbols(symbols, cancellationToken))
                        .Select(x => x.AsQuoteDetailsDto());
    }

    public async Task<QuoteDetailsDto> GetQuote(string symbol, CancellationToken cancellationToken = default)
    {
        return (await GetQuotes(new string[] { symbol }, cancellationToken)).FirstOrDefault();
    }

    public async Task<HistoricalDataDto> GetHistoricalData(string symbol,
                                                           DateTimeOffset start,
                                                           DateTimeOffset end,
                                                           IEnumerable<QuoteEvent> eventsToInclude,
                                                           string interval = "1d",
                                                           CancellationToken cancellationToken = default)
    {
        var requestUrl = $"v8/finance/chart/{symbol}?period1={start.ToUnixTimeSeconds()}&period2={end.ToUnixTimeSeconds()}&events={string.Join("|", eventsToInclude)}&interval={interval}";
        return (await GetHistoricalDataByUri(requestUrl, cancellationToken)).AsQuoteHistoricalData();
    }

    public async Task<HistoricalDataDto> GetHistoricalData(string symbol,
                                                           string range,
                                                           IEnumerable<QuoteEvent> eventsToInclude,
                                                           string interval = "1d",
                                                           CancellationToken cancellationToken = default)
    {
        var requestUrl = $"v8/finance/chart/{symbol}?range={range}&events={string.Join("|", eventsToInclude)}&interval={interval}";
        return (await GetHistoricalDataByUri(requestUrl, cancellationToken)).AsQuoteHistoricalData();
    }

    private async Task<YahooFinanceQuoteHistoricalData> GetHistoricalDataByUri(string uri, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(uri, cancellationToken);
        var responseStr = await response.Content.ReadAsStringAsync(cancellationToken);
        var contentStr = JsonNode.Parse(responseStr)["chart"]["result"][0].ToString();
        return _jsonSerializer.Deserialize<YahooFinanceQuoteHistoricalData>(contentStr);
    }

    private async Task<IEnumerable<YahooFinanceQuoteDetails>> GetQuotesBySymbols(IEnumerable<string> symbols, CancellationToken cancellationToken = default)
    {
        if (!symbols.Any())
            return new List<YahooFinanceQuoteDetails>();

        var requestUrl = $"v7/finance/quote?symbols={string.Join(",", symbols)}";
        var response = await _httpClient.GetAsync(requestUrl, cancellationToken);
        var responseStr = await response.Content.ReadAsStringAsync(cancellationToken);
        var quotesJsonArray = JsonNode.Parse(responseStr)["quoteResponse"]["result"].ToString();
        return _jsonSerializer.Deserialize<IEnumerable<YahooFinanceQuoteDetails>>(quotesJsonArray);
    }
}