namespace Stockfolio.Shared.Core.ValueObjects.Currencies;
public record CurrencyExchangeRate(Currency From, Currency To, ExchangeRate Rate);