using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Modules.Users.Core.Exceptions;

internal class EmailInUseException : StockfolioException
{
    public EmailInUseException() : base("Email is already in use.")
    {
    }
}