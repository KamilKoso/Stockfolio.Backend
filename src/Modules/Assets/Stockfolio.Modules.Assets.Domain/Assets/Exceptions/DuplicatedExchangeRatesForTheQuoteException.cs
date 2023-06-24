using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Modules.Assets.Core.Assets.Exceptions;

internal class DuplicatedExchangeRatesForTheQuoteException : StockfolioException
{
    public DuplicatedExchangeRatesForTheQuoteException() : base("Provided exchange rates have duplicates.")
    {
    }
}