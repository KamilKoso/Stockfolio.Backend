using Stockfolio.Shared.Core.Exceptions;

namespace Stockfolio.Modules.Assets.Application.Exceptions;

internal class MissingSearchQueryException : StockfolioException
{
    internal MissingSearchQueryException() : base("Search query cannot be empty.")
    {
    }
}