namespace Stockfolio.Shared.Core.Exceptions;

public class InvalidExchangeRateException : StockfolioException
{
    public InvalidExchangeRateException() : base($"Provided exchange rate is invalid. Exhange rates must be positive number.")
    {
    }
}