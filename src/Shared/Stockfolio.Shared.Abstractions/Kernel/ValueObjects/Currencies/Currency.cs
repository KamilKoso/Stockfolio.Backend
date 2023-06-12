using Stockfolio.Shared.Abstractions.Kernel.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Stockfolio.Shared.Abstractions.Kernel.ValueObjects.Currencies;

public record Currency
{
    public static readonly IReadOnlyDictionary<string, Currency> SupportedCurrencies = new HashSet<Currency>()
    {
     new("ARS", "Argentine Peso", "$"),
     new("AUD", "Australian Dollar", "$"),
     new("BBD", "Barbadian Dollar", "$"),
     new("BRL", "Brazilian Real", "R$"),
     new("GBP", "British Pound", "£"),
     new("CAD", "Canadian Dollar", "$"),
     new("CLP", "Chilean Peso", "$"),
     new("CNY", "Chinese Yuan", "元"),
     new("CZK", "Czech Koruna", "Kč"),
     new("DKK", "Danish Krone", "kr"),
     new("XCD", "East Caribbean Dollar", "$"),
     new("EGP", "Egyptian Pound", "£"),
     new("EUR", "Euro", "€"),
     new("HKD", "Hong Kong Dollar", "元"),
     new("HUF", "Hungarian Forint", "Ft"),
     new("ISK", "Icelandic Krona", "kr"),
     new("INR", "Indian Rupee", "₹"),
     new("IDR", "Indonesian Rupiah", "Rp"),
     new("ILS", "Israeli Sheqel", "₪"),
     new("JMD", "Jamaican Dollar", "J$"),
     new("JPY", "Japanese Yen", "¥"),
     new("LBP", "Lebanese Pound", "£"),
     new("MYR", "Malaysian Ringgit", "RM"),
     new("MXN", "Mexican Peso", "$"),
     new("NAD", "Namibian Dollar", "$"),
     new("NPR", "Nepalese Rupee", "₨"),
     new("NZD", "New Zealand Dollar", "$"),
     new("NOK", "Norwegian Krone", "kr"),
     new("OMR", "Omani Rial", "﷼"),
     new("PKR", "Pakistani Rupee", "₨"),
     new("PAB", "Panamanian Balboa", "B/."),
     new("PHP", "Philippine Peso", "Ph"),
     new("PLN", "Polish Zloty", "zł"),
     new("QAR", "Qatari Riyal", "﷼"),
     new("RON", "Romanian Leu", "le"),
     new("RUB", "Russian Rouble", "ру,;"),
     new("SAR", "Saudi Riyal", "﷼"),
     new("SGD", "Singapore Dollar", "$"),
     new("ZAR", "South African Rand", "R"),
     new("KRW", "South Korean Won", "₩"),
     new("LKR", "Sri Lankan Rupee", "₨"),
     new("SEK", "Swedish Krona", "kr"),
     new("CHF", "Swiss Franc", "CHF"),
     new("THB", "Thai Baht", "฿"),
     new("TRY", "Turkish Lira", "YTL"),
     new("USD", "US Dollar", "$"),
    }.ToDictionary(currency => currency.Code);

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
        if (!SupportedCurrencies.TryGetValue(code, out var currency))
        {
            throw new UnsupportedCurrencyException(code);
        }

        Code = currency.Code;
        DisplayName = currency.DisplayName;
        Symbol = currency.Symbol;
    }

    private Currency(string code, string displayName, string symbol)
    {
        Code = code;
        DisplayName = displayName;
        Symbol = symbol;
    }

    public override string ToString() => Code;

    public static implicit operator string(Currency currency) => currency.ToString();

    public static implicit operator Currency(string currencyCode) => new(currencyCode);
}