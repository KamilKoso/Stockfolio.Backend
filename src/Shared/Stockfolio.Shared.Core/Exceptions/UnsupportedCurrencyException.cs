namespace Stockfolio.Shared.Core.Exceptions;

public class UnsupportedCurrencyException : StockfolioException
{
    public string Currency { get; }

    public UnsupportedCurrencyException(string currency) : base($"Currency: '{currency}' is unsupported.")
    {
        Currency = currency;
    }
}