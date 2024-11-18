using Stockfolio.Shared.Core.Exceptions;

namespace Stockfolio.Modules.Users.Core.Exceptions;

internal class InvalidCredentialsException : StockfolioException
{
    public InvalidCredentialsException() : base("Invalid credentials.")
    {
    }
}