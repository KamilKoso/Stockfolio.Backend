using Stockfolio.Shared.Core.Exceptions;

namespace Stockfolio.Modules.Users.Core.Exceptions;

internal class UserNotActiveException : StockfolioException
{
    public UserNotActiveException(Guid userId) : base($"User with ID: '{userId}' is not active.")
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}