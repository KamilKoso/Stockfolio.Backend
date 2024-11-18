namespace Stockfolio.Shared.Core.Exceptions;

public class InvalidAmountException : StockfolioException
{
    public decimal Amount { get; }

    public InvalidAmountException(decimal amount) : base($"Amount: '{amount}' is invalid.")
    {
        Amount = amount;
    }
}