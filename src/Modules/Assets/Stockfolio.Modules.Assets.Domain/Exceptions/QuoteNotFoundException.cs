using Stockfolio.Modules.Assets.Core.ValueObjects;
using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Modules.Assets.Core.Exceptions;

internal class QuoteNotFoundException : StockfolioException
{
    public Guid QuoteId { get; }
    public AssetName AssetName { get; }

    public QuoteNotFoundException(Guid quoteId, AssetName assetName) : base($"Quote with the id ${quoteId} was note found on ${assetName}.")
    {
        QuoteId = quoteId;
        AssetName = assetName;
    }
}