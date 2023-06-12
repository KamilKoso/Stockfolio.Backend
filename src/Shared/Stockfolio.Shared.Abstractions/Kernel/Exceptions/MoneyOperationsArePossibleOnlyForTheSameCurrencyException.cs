using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Shared.Abstractions.Kernel.Exceptions;

public class MoneyOperationsArePossibleOnlyForTheSameCurrencyException : StockfolioException
{
    public MoneyOperationsArePossibleOnlyForTheSameCurrencyException() : base($"Money operations are possible only for the same currency.")
    {
    }
}