using Stockfolio.Modules.Assets.Application.Dto.Assets;
using Stockfolio.Modules.Assets.Application.Repositories;
using Stockfolio.Shared.Abstractions;
using Stockfolio.Shared.Abstractions.Queries;

namespace Stockfolio.Modules.Assets.Application.Queries.Assets.Handlers;

internal class GetHistoricalQuotesHandler : IQueryHandler<GetHistoricalQuotes, HistoricalQuotesDto>
{
    private readonly IStockMarketRepository _quotesRepository;

    public GetHistoricalQuotesHandler(IStockMarketRepository quotesRepository)
    {
        _quotesRepository = quotesRepository;
    }

    public async Task<HistoricalQuotesDto> HandleAsync(GetHistoricalQuotes query, CancellationToken cancellationToken = default)
    {
        if (!query.Range.IsNullOrEmpty())
        {
            return await _quotesRepository.GetHistoricalQuotes(symbol: query.Symbol,
                                                             range: query.Range,
                                                             interval: query.Interval,
                                                             cancellationToken: cancellationToken);
        }
        else
        {
            return await _quotesRepository.GetHistoricalQuotes(symbol: query.Symbol,
                                                            start: query.Start,
                                                            end: query.End,
                                                            interval: query.Interval,
                                                            cancellationToken: cancellationToken);
        }
    }
}