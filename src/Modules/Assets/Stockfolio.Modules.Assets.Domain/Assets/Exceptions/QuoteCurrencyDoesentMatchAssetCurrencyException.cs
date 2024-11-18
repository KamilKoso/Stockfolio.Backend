using Stockfolio.Shared.Core.Exceptions;

namespace Stockfolio.Modules.Assets.Core.Assets.Exceptions;

internal class QuoteCurrencyDoesentMatchAssetCurrencyException : StockfolioException
{
    public QuoteCurrencyDoesentMatchAssetCurrencyException() : base($"Quote currency must match asset currency.")
    {
    }
}