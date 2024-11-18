using Stockfolio.Shared.Core.Exceptions;

namespace Stockfolio.Modules.Users.Core.Exceptions;

internal class RefreshTokenNotFoundException : StockfolioException
{
    public RefreshTokenNotFoundException(Guid userId) : base($"User with ID: '{userId}' does not have assigned refresh token.")
    {
    }
}