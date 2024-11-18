using Stockfolio.Shared.Core.Exceptions;

namespace Stockfolio.Modules.Users.Core.Exceptions;

internal class SignUpDisabledException : StockfolioException
{
    public SignUpDisabledException() : base("Signing up is disabled.")
    {
    }
}