using Stockfolio.Modules.Assets.Core.Assets.Quotes;
using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Modules.Assets.Core.Assets.Exceptions;

internal class MissingExchangeRateForTheQuoteException : StockfolioException
{
    public MissingExchangeRateForTheQuoteException(QuoteId quoteId) : base($"Exchange rate for quote with the id: {quoteId} is missing.")
    {
    }
}