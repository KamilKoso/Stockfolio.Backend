using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Modules.Users.Core.Exceptions;

internal class ConfirmEmailException : StockfolioException
{
    public ConfirmEmailException(string reason) : base(reason)
    {
    }
}