﻿using Stockfolio.Modules.Assets.Core.Assets;
using Stockfolio.Shared.Core.Types;

namespace Stockfolio.Modules.Assets.Application.Repositories;

internal interface IAssetsRepository
{
    Task<Asset> GetAssetById(AssetId assetId);

    Task<Asset> GetUserAssets();

    Task CreateAsset(Asset asset);

    Task DeleteAsset(AssetId assetId);

    Task UpdateAsset(Asset asset);
}