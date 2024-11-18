using Stockfolio.Shared.Core.Exceptions;

namespace Stockfolio.Modules.Users.Core.Exceptions;

internal class UserNotFoundException : StockfolioException
{
    public UserNotFoundException(Guid userId) : base($"User with ID: '{userId}' was not found.")
    {
        UserId = userId;
    }

    public UserNotFoundException(string email) : base($"User with email: '{email}' was not found.")
    {
        Email = email;
    }

    public string Email { get; }
    public Guid UserId { get; }
}