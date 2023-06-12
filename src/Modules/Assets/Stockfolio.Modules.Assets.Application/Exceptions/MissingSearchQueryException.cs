using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Modules.Assets.Application.Exceptions;

internal class MissingSearchQueryException : StockfolioException
{
    internal MissingSearchQueryException() : base("Search query cannot be empty.")
    {
    }
}