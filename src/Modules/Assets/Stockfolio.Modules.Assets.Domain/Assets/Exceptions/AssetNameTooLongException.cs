using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Modules.Assets.Core.Assets.Exceptions;

internal class AssetNameTooLongException : StockfolioException
{
    public AssetNameTooLongException(AssetName assetName, int maximumSize) : base($"Asset name must be at most {maximumSize} characters long. Provided asset name is ${assetName.Length} characters long.")
    {
    }
}