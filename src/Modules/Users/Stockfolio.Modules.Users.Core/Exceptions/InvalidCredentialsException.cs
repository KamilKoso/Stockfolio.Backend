using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Modules.Users.Core.Exceptions;

internal class InvalidCredentialsException : StockfolioException
{
    public InvalidCredentialsException() : base("Invalid credentials.")
    {
    }
}