using Stockfolio.Modules.Assets.Application.Dto.Forex;
using Stockfolio.Shared.Abstractions.Queries;

namespace Stockfolio.Modules.Assets.Application.Queries.Forex;

internal record GetSupportedCurrencies() : IQuery<Dictionary<string, SupportedCurrencyDto>>;