using Stockfolio.Modules.Assets.Application.Repositories;
using Stockfolio.Modules.Assets.Core;
using Stockfolio.Shared.Abstractions.Kernel.Types;

namespace Stockfolio.Modules.Assets.Infrastructure.Repositories;

internal class AssetsRepository : IAssetsRepository
{
    public Task CreateAsset(Asset asset)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsset(AssetId assetId)
    {
        throw new NotImplementedException();
    }

    public Task<Asset> GetAssetById(AssetId assetId)
    {
        throw new NotImplementedException();
    }

    public Task<Asset> GetUserAssets()
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsset(Asset asset)
    {
        throw new NotImplementedException();
    }
}