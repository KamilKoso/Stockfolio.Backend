using Stockfolio.Modules.Assets.Application.Dto.Forex;
using Stockfolio.Shared.Abstractions.Kernel.ValueObjects.Currencies;
using Stockfolio.Shared.Abstractions.Queries;

namespace Stockfolio.Modules.Assets.Application.Queries.Forex.Handlers;

internal class GetSupportedCurrenciesHandler : IQueryHandler<GetSupportedCurrencies, Dictionary<string, SupportedCurrencyDto>>
{
    public Task<Dictionary<string, SupportedCurrencyDto>> HandleAsync(GetSupportedCurrencies query, CancellationToken cancellationToken = default)
    => Task.FromResult(SupportedCurrencies.All
                        .ToDictionary(key => key.Key,
                                      value => new SupportedCurrencyDto(value.Value.Code, value.Value.DisplayName, value.Value.Symbol)));
}