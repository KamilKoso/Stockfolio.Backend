using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Shared.Abstractions.Kernel.Exceptions;

public class InvalidFullNameException : StockfolioException
{
    public string FullName { get; }

    public InvalidFullNameException(string fullName) : base($"Full name: '{fullName}' is invalid.")
    {
        FullName = fullName;
    }
}