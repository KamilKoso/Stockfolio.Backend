using Stockfolio.Modules.Portfolios.Core.Exceptions;

namespace Stockfolio.Modules.Portfolios.Core.ValueObjects;

internal record PortfolioItem
{
    public string Name { get; }
    public uint Quantity { get; }

    public PortfolioItem(string name, uint quantity)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new EmptyPortfolioItemNameException();
        }

        Name = name;
        Quantity = quantity;
    }
}