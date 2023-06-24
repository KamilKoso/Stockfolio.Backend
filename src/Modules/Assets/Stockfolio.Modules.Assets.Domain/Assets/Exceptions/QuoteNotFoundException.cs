using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Modules.Assets.Core.Assets.Exceptions;

internal class QuoteNotFoundException : StockfolioException
{
    public QuoteNotFoundException(Guid quoteId, AssetName assetName) : base($"Quote with the id ${quoteId} was note found on ${assetName}.")
    {
    }
}