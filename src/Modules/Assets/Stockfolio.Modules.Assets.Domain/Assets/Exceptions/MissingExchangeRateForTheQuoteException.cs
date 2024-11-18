using Stockfolio.Modules.Assets.Core.Assets.Quotes;
using Stockfolio.Shared.Core.Exceptions;

namespace Stockfolio.Modules.Assets.Core.Assets.Exceptions;

internal class MissingExchangeRateForTheQuoteException : StockfolioException
{
    public MissingExchangeRateForTheQuoteException(QuoteId quoteId) : base($"Exchange rate for quote with the id: {quoteId} is missing.")
    {
    }
}