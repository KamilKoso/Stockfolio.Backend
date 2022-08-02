using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Modules.StockMarket.Application.Exceptions;

public class MissingSearchQueryException : StockfolioException
{
    public MissingSearchQueryException() : base("Search query cannot be empty.")
    {
    }
}