using Stockfolio.Modules.Assets.Application.Dto.Assets;
using Stockfolio.Modules.Assets.Application.Repositories;
using Stockfolio.Shared.Abstractions.Queries;

namespace Stockfolio.Modules.Assets.Application.Queries.Assets.Handlers;

internal sealed class GetAssetsHandler : IQueryHandler<GetAssets, IEnumerable<AssetDto>>
{
    internal static Func<GetAssets, string> CacheKeyBuilder = (query) => $"{typeof(GetAssets)}_{string.Join(',', query.Symbols.OrderBy(x => x))}";
    internal static TimeSpan CacheExpiration = TimeSpan.FromMinutes(5);

    private readonly IStockMarketRepository _quotesRepository;

    public GetAssetsHandler(IStockMarketRepository quotesRepository)
    {
        _quotesRepository = quotesRepository;
    }

    public async Task<IEnumerable<AssetDto>> HandleAsync(GetAssets query, CancellationToken cancellationToken = default)
        => await _quotesRepository.GetSecurities(query.Symbols, cancellationToken);
}