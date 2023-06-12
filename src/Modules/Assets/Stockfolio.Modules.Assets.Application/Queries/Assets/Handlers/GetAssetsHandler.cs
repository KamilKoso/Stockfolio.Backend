using Stockfolio.Modules.Assets.Application.Dto.Assets;
using Stockfolio.Modules.Assets.Application.Repositories;
using Stockfolio.Shared.Abstractions.Queries;

namespace Stockfolio.Modules.Assets.Application.Queries.Assets.Handlers;

internal class GetAssetsHandler : IQueryHandler<GetAssets, IEnumerable<AssetDto>>
{
    private readonly IStockMarketRepository _quotesRepository;

    public GetAssetsHandler(IStockMarketRepository quotesRepository)
    {
        _quotesRepository = quotesRepository;
    }

    public async Task<IEnumerable<AssetDto>> HandleAsync(GetAssets query, CancellationToken cancellationToken = default)
        => await _quotesRepository.GetSecurities(query.Symbols, cancellationToken);
}