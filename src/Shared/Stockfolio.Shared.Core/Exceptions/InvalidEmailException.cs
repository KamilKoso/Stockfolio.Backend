namespace Stockfolio.Shared.Core.Exceptions;

public class InvalidEmailException : StockfolioException
{
    public string Email { get; }

    public InvalidEmailException(string email) : base($"Email: '{email}' is invalid.")
    {
        Email = email;
    }
}