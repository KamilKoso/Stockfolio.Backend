using Stockfolio.Shared.Core.Exceptions;

namespace Stockfolio.Modules.Users.Core.Exceptions;

public class InvalidRefreshTokenException : StockfolioException
{
    public InvalidRefreshTokenException(Guid userId) : base($"Refresh token validation failed for user with ID: {userId}.")
    {
    }
}