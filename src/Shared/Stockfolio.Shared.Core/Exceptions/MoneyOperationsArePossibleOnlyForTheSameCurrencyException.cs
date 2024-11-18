namespace Stockfolio.Shared.Core.Exceptions;

public class MoneyOperationsArePossibleOnlyForTheSameCurrencyException : StockfolioException
{
    public MoneyOperationsArePossibleOnlyForTheSameCurrencyException() : base($"Money operations are possible only for the same currency.")
    {
    }
}