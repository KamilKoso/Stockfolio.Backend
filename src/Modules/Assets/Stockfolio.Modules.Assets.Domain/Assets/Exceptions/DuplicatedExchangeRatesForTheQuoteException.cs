using Stockfolio.Shared.Core.Exceptions;

namespace Stockfolio.Modules.Assets.Core.Assets.Exceptions;

internal class DuplicatedExchangeRatesForTheQuoteException : StockfolioException
{
    public DuplicatedExchangeRatesForTheQuoteException() : base("Provided exchange rates have duplicates.")
    {
    }
}