using Stockfolio.Shared.Abstractions.Exceptions;

namespace Stockfolio.Modules.Portfolios.Core.Exceptions;

internal class EmptyPortfolioNameException : StockfolioException
{
    public EmptyPortfolioNameException() : base("Portfolio name cannot be empty.")
    {
    }
}