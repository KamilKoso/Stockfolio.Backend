using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Modules.Users.Core.Exceptions;

public class InvalidRefreshTokenException : StockfolioException
{
    public InvalidRefreshTokenException(Guid userId) : base($"Refresh token validation failed for user with ID: {userId}.")
    {
    }
}