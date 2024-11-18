using Stockfolio.Shared.Core.Exceptions;

namespace Stockfolio.Modules.Users.Core.Exceptions;

internal class ConfirmEmailException : StockfolioException
{
    public ConfirmEmailException(string reason) : base(reason)
    {
    }
}