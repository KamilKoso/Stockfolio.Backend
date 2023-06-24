using Stockfolio.Modules.Assets.Application.Dto.Forex;
using Stockfolio.Modules.Assets.Application.Repositories;
using Stockfolio.Shared.Abstractions.Queries;
using Stockfolio.Shared.Abstractions.Time;

namespace Stockfolio.Modules.Assets.Application.Queries.Forex.Handlers;

internal class GetExchangeRatesHandler : IQueryHandler<GetExchangeRates, ExchangeRatesDto>
{
    private readonly IForexRepository _forexRepository;
    private readonly IClock _clock;

    public GetExchangeRatesHandler(IForexRepository forexRepository,
                                  IClock clock)
    {
        _forexRepository = forexRepository;
        _clock = clock;
    }

    public async Task<ExchangeRatesDto> HandleAsync(GetExchangeRates query, CancellationToken cancellationToken = default)
    {
        if (query.Start is null)
        {
            query = query with { Start = _clock.CurrentDate() };
        }

        if (query.End is null)
        {
            query = query with { End = _clock.CurrentDate() };
        }

        return await _forexRepository.GetHistoricalExchangeRates(query.From, query.To, (DateTimeOffset)query.Start, (DateTimeOffset)query.End, cancellationToken);
    }
}