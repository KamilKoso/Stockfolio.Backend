using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Shared.Abstractions.Kernel.Exceptions;

public class InvalidNationalityException : StockfolioException
{
    public string Nationality { get; }

    public InvalidNationalityException(string nationality) : base($"Nationality: '{nationality}' is invalid.")
    {
        Nationality = nationality;
    }
}