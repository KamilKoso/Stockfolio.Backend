using Stockfolio.Modules.Assets.Core.ValueObjects;
using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Modules.Assets.Core.Exceptions;

internal class MissingExchangeRateForTheQuoteException : StockfolioException
{
    public QuoteId QuoteId { get; }

    public MissingExchangeRateForTheQuoteException(QuoteId quoteId) : base($"Exchange rate for quote with the id: {quoteId} is missing.")
    {
        QuoteId = quoteId;
    }
}