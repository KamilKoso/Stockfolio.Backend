using Stockfolio.Modules.StockMarket.Application.Dto;
using Stockfolio.Modules.StockMarket.Application.DTO;
using Stockfolio.Modules.StockMarket.Application.Repositories;
using Stockfolio.Modules.StockMarket.Infrastructure.YahooFinance.Dto;
using Stockfolio.Modules.StockMarket.Infrastructure.YahooFinance.Dto.Mappings;
using Stockfolio.Modules.StockMarket.Infrastructure.YahooFinance.Options;
using Stockfolio.Shared.Infrastructure.Serialization;
using System.Text.Json.Nodes;

namespace Stockfolio.Modules.StockMarket.Infrastructure.Repositories;

internal class YahooFinanceQuotesRepository : IQuotesRepository
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly YahooFinanceOptions _yahooFinanceOptions;
    private readonly string _httpClientName = "QuotesHttpClient"; // Add polly for resiliency

    public YahooFinanceQuotesRepository(IHttpClientFactory httpClientFactory,
                            IJsonSerializer jsonSerializer,
                            YahooFinanceOptions yahooFinanceOptions)
    {
        _httpClientFactory = httpClientFactory;
        _jsonSerializer = jsonSerializer;
        _yahooFinanceOptions = yahooFinanceOptions;
    }

    public async Task<SearchQuotesDto> SearchQuotes(string searchQuery, int quotesCount = 6, CancellationToken cancellationToken = default)
    {
        var httpClient = _httpClientFactory.CreateClient(_httpClientName);
        var requestUrl = $"{_yahooFinanceOptions.BaseApiUrl}/v1/finance/search?q={searchQuery}&quotesCount={quotesCount}&newsCount=0&listCount=0";

        var response = await httpClient.GetAsync(requestUrl, cancellationToken);

        var responseStr = await response.Content.ReadAsStringAsync(cancellationToken);
        var quotesJsonArray = JsonNode.Parse(responseStr)["quotes"]?.ToString();
        var yahooFinanceQuotesDto = _jsonSerializer.Deserialize<IEnumerable<YahooFinanceQuoteDto>>(quotesJsonArray);

        return new()
        {
            Quotes = yahooFinanceQuotesDto.Select(x => x.AsQuoteDto()).ToList()
        };
    }

    public async Task<IEnumerable<QuoteDetailsDto>> GetQuotes(IEnumerable<string> symbols, CancellationToken cancellationToken = default)
    {
        if (!symbols.Any())
            return new List<QuoteDetailsDto>();

        var httpClient = _httpClientFactory.CreateClient(_httpClientName);
        var requestUrl = $"{_yahooFinanceOptions.BaseApiUrl}/v7/finance/quote?symbols={string.Join(",", symbols)}";

        var response = await httpClient.GetAsync(requestUrl, cancellationToken);
        var responseStr = await response.Content.ReadAsStringAsync(cancellationToken);
        var quotesJsonArray = JsonNode.Parse(responseStr)["quoteResponse"]["result"].ToString();
        return _jsonSerializer
                    .Deserialize<IEnumerable<YahooFinanceQuoteDetailsDto>>(quotesJsonArray)
                    .Select(x => x.AsDetailsQuoteDto());
    }

    public async Task<QuoteDetailsDto> GetQuote(string symbol, CancellationToken cancellationToken = default)
    {
        return (await GetQuotes(new string[] { symbol }, cancellationToken)).FirstOrDefault();
    }
}