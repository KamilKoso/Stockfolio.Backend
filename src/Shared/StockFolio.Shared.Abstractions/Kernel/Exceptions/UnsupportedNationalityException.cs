using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Shared.Abstractions.Kernel.Exceptions;

public class UnsupportedNationalityException : StockfolioException
{
    public string Nationality { get; }

    public UnsupportedNationalityException(string nationality) : base($"Nationality: '{nationality}' is unsupported.")
    {
        Nationality = nationality;
    }
}