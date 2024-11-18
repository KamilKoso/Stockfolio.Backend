using Stockfolio.Shared.Core.Exceptions;

namespace Stockfolio.Modules.Users.Core.Exceptions;

internal class SignUpException : StockfolioException
{
    public SignUpException(string reason, string code) : base(reason, code)
    {
    }
}