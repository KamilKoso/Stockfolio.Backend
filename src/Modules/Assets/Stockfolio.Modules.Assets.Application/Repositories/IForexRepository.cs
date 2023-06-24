using Stockfolio.Modules.Assets.Application.Dto.Forex;
using Stockfolio.Shared.Abstractions.Kernel.ValueObjects.Currencies;

namespace Stockfolio.Modules.Assets.Application.Repositories;

internal interface IForexRepository
{
    public Task<ExchangeRatesDto> GetHistoricalExchangeRates(Currency from, Currency to, DateTimeOffset start, DateTimeOffset end, CancellationToken cancellationToken = default);
}