using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Shared.Abstractions.Kernel.Exceptions;

public class InvalidExchangeRateException : StockfolioException
{
    public InvalidExchangeRateException() : base($"Provided exchange rate is invalid. Exhange rates must be positive number.")
    {
    }
}