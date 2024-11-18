using Stockfolio.Shared.Core.Exceptions;

namespace Stockfolio.Modules.Users.Core.Exceptions;

internal class EmailAlreadyConfirmedException : StockfolioException
{
    public EmailAlreadyConfirmedException() : base("Email has been already confirmed for this account.")
    {
    }
}