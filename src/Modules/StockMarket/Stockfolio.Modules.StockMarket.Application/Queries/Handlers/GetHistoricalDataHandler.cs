using Stockfolio.Modules.Portfolios.Application.Models;
using Stockfolio.Modules.StockMarket.Application.Dto;
using Stockfolio.Modules.StockMarket.Application.Repositories;
using Stockfolio.Shared.Abstractions;
using Stockfolio.Shared.Abstractions.Queries;

namespace Stockfolio.Modules.StockMarket.Application.Queries.Handlers;

internal class GetHistoricalDataHandler : IQueryHandler<GetHistoricalData, HistoricalDataDto>
{
    private readonly IQuotesRepository _quotesRepository;

    public GetHistoricalDataHandler(IQuotesRepository quotesRepository)
    {
        _quotesRepository = quotesRepository;
    }

    public async Task<HistoricalDataDto> HandleAsync(GetHistoricalData query, CancellationToken cancellationToken = default)
    {
        var quoteEvents = new[] { QuoteEvent.CapitalGains, QuoteEvent.Dividends, QuoteEvent.Splits };
        if (!query.Range.IsNullOrEmpty())
        {
            return await _quotesRepository.GetHistoricalData(symbol: query.Symbol,
                                                             range: query.Range,
                                                             eventsToInclude: quoteEvents,
                                                             interval: query.Interval,
                                                             cancellationToken: cancellationToken);
        }
        else
        {
            return await _quotesRepository.GetHistoricalData(symbol: query.Symbol,
                                                            start: query.Start,
                                                            end: query.End,
                                                            eventsToInclude: quoteEvents,
                                                            interval: query.Interval,
                                                            cancellationToken: cancellationToken);
        }
    }
}