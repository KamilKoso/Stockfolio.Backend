using Stockfolio.Modules.Portfolios.Core.Exceptions;

namespace Stockfolio.Modules.Portfolios.Core.ValueObjects;

internal record PortfolioName
{
    public string Value { get; }

    public PortfolioName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new EmptyPortfolioNameException();
        }

        Value = value;
    }

    public static implicit operator string(PortfolioName name) => name.Value;

    public static implicit operator PortfolioName(string value) => new(value);
}