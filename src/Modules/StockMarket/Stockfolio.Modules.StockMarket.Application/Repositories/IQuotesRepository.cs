using Stockfolio.Modules.Portfolios.Application.Models;
using Stockfolio.Modules.StockMarket.Application.Dto;
using Stockfolio.Modules.StockMarket.Application.DTO;

namespace Stockfolio.Modules.StockMarket.Application.Repositories;

internal interface IQuotesRepository
{
    Task<SearchQuotesDto> SearchQuotes(string searchQuery, int quotesCount, CancellationToken cancellationToken = default);

    Task<IEnumerable<QuoteDetailsDto>> GetQuotes(IEnumerable<string> symbols, CancellationToken cancellationToken = default);

    Task<QuoteDetailsDto> GetQuote(string symbol, CancellationToken cancellationToken = default);

    Task<HistoricalDataDto> GetHistoricalData(string symbol, DateTimeOffset start, DateTimeOffset end, IEnumerable<QuoteEvent> eventsToInclude, string interval = "1d", CancellationToken cancellationToken = default);

    Task<HistoricalDataDto> GetHistoricalData(string symbol, string range, IEnumerable<QuoteEvent> eventsToInclude, string interval = "1d", CancellationToken cancellationToken = default);
}