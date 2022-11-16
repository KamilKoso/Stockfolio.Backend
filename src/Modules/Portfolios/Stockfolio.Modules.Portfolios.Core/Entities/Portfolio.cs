using Stockfolio.Modules.Portfolios.Core.ValueObjects;

namespace Stockfolio.Modules.Portfolios.Core.Entities;

internal class Portfolio
{
    public Guid Id { get; private set; }
    public PortfolioName PortfolioName { get; private set; }
    public List<PortfolioItem> PortfolioItems = new();
}