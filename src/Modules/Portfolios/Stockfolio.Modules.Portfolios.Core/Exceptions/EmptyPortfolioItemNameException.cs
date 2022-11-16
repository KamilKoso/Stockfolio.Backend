using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Modules.Portfolios.Core.Exceptions;

internal class EmptyPortfolioItemNameException : StockfolioException
{
    public EmptyPortfolioItemNameException() : base("Portfolio item name cannot be empty.")
    {
    }
}