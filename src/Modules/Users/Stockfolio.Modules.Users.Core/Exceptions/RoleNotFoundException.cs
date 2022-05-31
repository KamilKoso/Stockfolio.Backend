using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Modules.Users.Core.Exceptions;

internal class RoleNotFoundException : StockfolioException
{
    public RoleNotFoundException(string role) : base($"Role: '{role}' was not found.")
    {
    }
}