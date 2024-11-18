using Stockfolio.Shared.Core.Exceptions;

namespace Stockfolio.Modules.Users.Core.Exceptions;

internal class InvalidPasswordException : StockfolioException
{
    public InvalidPasswordException(string reason) : base($"Invalid password: {reason}")
    {
    }
}