using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Shared.Abstractions.Kernel.Exceptions;

public class InvalidEmailException : StockfolioException
{
    public string Email { get; }

    public InvalidEmailException(string email) : base($"Email: '{email}' is invalid.")
    {
        Email = email;
    }
}