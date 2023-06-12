namespace Stockfolio.Shared.Abstractions.Kernel.ValueObjects.Currencies;
public record CurrencyExchangeRate(Currency From, Currency To, ExchangeRate Rate);