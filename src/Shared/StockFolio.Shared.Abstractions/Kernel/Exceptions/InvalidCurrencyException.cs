using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Shared.Abstractions.Kernel.Exceptions;

public class InvalidCurrencyException : StockfolioException
{
    public string Currency { get; }

    public InvalidCurrencyException(string currency) : base($"Currency: '{currency}' is invalid.")
    {
        Currency = currency;
    }
}