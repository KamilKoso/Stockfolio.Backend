using Stockfolio.Modules.Assets.Core;
using Stockfolio.Shared.Abstractions.Kernel.Types;

namespace Stockfolio.Modules.Assets.Application.Repositories;

internal interface IAssetsRepository
{
    Task<Asset> GetAssetById(AssetId assetId);

    Task<Asset> GetUserAssets();

    Task CreateAsset(Asset asset);

    Task DeleteAsset(AssetId assetId);

    Task UpdateAsset(Asset asset);
}