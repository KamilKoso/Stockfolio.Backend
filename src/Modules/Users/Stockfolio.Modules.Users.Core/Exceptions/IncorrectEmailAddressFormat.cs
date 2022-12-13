using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Modules.Users.Core.Exceptions;

internal class InvalidEmailAddressFormatException : StockfolioException
{
    public InvalidEmailAddressFormatException(string email) : base($"{email} is invalid format of email address.")
    {
    }
}