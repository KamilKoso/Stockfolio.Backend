using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Modules.Users.Core.Exceptions;

internal class SignUpException : StockfolioException
{
    public SignUpException(string reason, string code) : base(reason, code)
    {
    }
}