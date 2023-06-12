using Stockfolio.Modules.Assets.Core.ValueObjects;
using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Modules.Assets.Core.Exceptions;

internal class AssetNameTooLongException : StockfolioException
{
    public AssetName AssetName { get; }
    public int MaximumSize { get; }

    public AssetNameTooLongException(AssetName assetName, int maximumSize) : base($"Asset name must be at most {maximumSize} characters long. Provided asset name is ${assetName.Length} characters long.")
    {
        AssetName = assetName;
        MaximumSize = maximumSize;
    }
}