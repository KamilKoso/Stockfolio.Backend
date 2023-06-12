using Stockfolio.Modules.Assets.Application.Dto.Forex;
using Stockfolio.Shared.Abstractions.Queries;

namespace Stockfolio.Modules.Assets.Application.Queries.Forex;

internal record GetExchangeRates(string From, string To, DateTimeOffset? Start = null, DateTimeOffset? End = null) : IQuery<ExchangeRatesDto>;