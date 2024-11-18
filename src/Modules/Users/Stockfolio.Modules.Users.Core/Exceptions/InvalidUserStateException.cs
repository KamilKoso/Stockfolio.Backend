using Stockfolio.Shared.Core.Exceptions;

namespace Stockfolio.Modules.Users.Core.Exceptions;

internal class InvalidUserStateException : StockfolioException
{
    public InvalidUserStateException(string state) : base($"User state '{state}' is invalid.")
    {
        State = state;
    }

    public string State { get; }
}