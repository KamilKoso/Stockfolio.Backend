using Stockfolio.Shared.Core.Exceptions;

namespace Stockfolio.Modules.Portfolios.Core.Exceptions;

internal class EmptyPortfolioItemNameException : StockfolioException
{
    public EmptyPortfolioItemNameException() : base("Portfolio item name cannot be empty.")
    {
    }
}