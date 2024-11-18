namespace Stockfolio.Shared.Core.Exceptions;

public class NotSupportedCurrencyException : StockfolioException
{
    public string Currency { get; }

    public NotSupportedCurrencyException(string currency) : base($"Currency: '{currency}' is not supported.")
    {
        Currency = currency;
    }
}