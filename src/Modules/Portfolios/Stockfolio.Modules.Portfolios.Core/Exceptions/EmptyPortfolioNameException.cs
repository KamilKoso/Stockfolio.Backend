using Stockfolio.Shared.Core.Exceptions;

namespace Stockfolio.Modules.Portfolios.Core.Exceptions;

internal class EmptyPortfolioNameException : StockfolioException
{
    public EmptyPortfolioNameException() : base("Portfolio name cannot be empty.")
    {
    }
}