using Stockfolio.Modules.Assets.Application.Dto.Assets;
using Stockfolio.Modules.Assets.Application.Repositories;
using Stockfolio.Shared.Abstractions;
using Stockfolio.Shared.Abstractions.Queries;

namespace Stockfolio.Modules.Assets.Application.Queries.Assets.Handlers;

internal sealed class GetHistoricalQuotesHandler : IQueryHandler<GetHistoricalQuotes, HistoricalQuotesDto>
{
    internal static Func<GetHistoricalQuotes, string> CacheKeyBuilder = (query) => $"{typeof(GetHistoricalQuotes)}_{query.Symbol}_{query.Range}_{query.Interval}_{query.Start}_{query.End}";

    internal static Func<GetHistoricalQuotes, TimeSpan?> CacheExpirationBuilder = (query) => query.Interval.Unit switch
    {
        "m" => TimeSpan.FromMinutes(query.Interval.Value),
        "h" => TimeSpan.FromHours(query.Interval.Value),
        "d" or "wk" or "mo" => TimeSpan.FromDays(1),
        _ => TimeSpan.FromMinutes(5)
    };

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