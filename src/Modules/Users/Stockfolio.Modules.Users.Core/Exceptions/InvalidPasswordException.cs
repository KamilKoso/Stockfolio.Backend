using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Modules.Users.Core.Exceptions;

internal class InvalidPasswordException : StockfolioException
{
    public InvalidPasswordException(string reason) : base($"Invalid password: {reason}")
    {
        Reason = reason;
    }

    public string Reason { get; }
}