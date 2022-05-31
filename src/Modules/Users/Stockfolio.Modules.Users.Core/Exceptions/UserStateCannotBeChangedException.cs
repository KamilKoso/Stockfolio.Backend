using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Modules.Users.Core.Exceptions;

internal class UserStateCannotBeChangedException : StockfolioException
{
    public UserStateCannotBeChangedException(string state, Guid userId)
        : base($"User state cannot be changed to: '{state}' for user with ID: '{userId}'.")
    {
        State = state;
        UserId = userId;
    }

    public string State { get; }
    public Guid UserId { get; }
}