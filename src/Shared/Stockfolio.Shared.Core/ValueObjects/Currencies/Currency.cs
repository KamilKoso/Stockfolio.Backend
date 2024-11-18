using Stockfolio.Shared.Core.Exceptions;

namespace Stockfolio.Shared.Core.ValueObjects.Currencies;

public record Currency
{
    public string Code { get; init; }
    public string DisplayName { get; init; }
    public string Symbol { get; init; }

    public Currency(string code)
    {
        if (string.IsNullOrWhiteSpace(code) || code.Length != 3)
        {
            throw new NotSupportedCurrencyException(code);
        }

        code = code.ToUpperInvariant();
        if (!SupportedCurrencies.All.TryGetValue(code, out var currency))
        {
            throw new UnsupportedCurrencyException(code);
        }

        Code = currency.Code;
        DisplayName = currency.DisplayName;
        Symbol = currency.Symbol;
    }

    internal Currency(string code, string displayName, string symbol)
    {
        Code = code;
        DisplayName = displayName;
        Symbol = symbol;
    }

    public override string ToString() => Code;

    public static implicit operator string(Currency currency) => currency.ToString();

    public static implicit operator Currency(string currencyCode) => new(currencyCode);
}