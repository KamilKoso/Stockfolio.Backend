using Stockfolio.Shared.Abstractions.Kernel.Exceptions;

namespace Stockfolio.Shared.Abstractions.Kernel.ValueObjects.Currencies;
public record ExchangeRate
{
    public decimal Value { get; init; }
    public ExchangeRate(decimal exchangeRate)
    {
        if (exchangeRate <= 0)
        {
            throw new InvalidExchangeRateException();
        }

        Value = exchangeRate;
    }

    public static implicit operator decimal(ExchangeRate rate) => rate.Value;
    public static implicit operator ExchangeRate(decimal rate) => new(rate);
};