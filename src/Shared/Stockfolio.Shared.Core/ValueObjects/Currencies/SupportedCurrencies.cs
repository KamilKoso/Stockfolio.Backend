namespace Stockfolio.Shared.Core.ValueObjects.Currencies;

public static class SupportedCurrencies
{
    public static readonly IReadOnlyDictionary<string, Currency> All = new HashSet<Currency>()
    {
     ARS,
     AUD,
     BBD,
     BRL,
     GBP,
     CAD,
     CLP,
     CNY,
     CZK,
     DKK,
     XCD,
     EGP,
     EUR,
     HKD,
     HUF,
     ISK,
     INR,
     IDR,
     ILS,
     JMD,
     JPY,
     LBP,
     MYR,
     MXN,
     NAD,
     NPR,
     NZD,
     NOK,
     OMR,
     PKR,
     PAB,
     PHP,
     PLN,
     QAR,
     RON,
     RUB,
     SAR,
     SGD,
     ZAR,
     KRW,
     LKR,
     SEK,
     CHF,
     THB,
     TRY,
     USD
    }.ToDictionary(currency => currency.Code);

    public static readonly Currency ARS = new Currency("ARS", "Argentine Peso", "$");
    public static readonly Currency AUD = new Currency("AUD", "Australian Dollar", "$");
    public static readonly Currency BBD = new Currency("BBD", "Barbadian Dollar", "$");
    public static readonly Currency BRL = new Currency("BRL", "Brazilian Real", "R$");
    public static readonly Currency GBP = new Currency("GBP", "British Pound", "£");
    public static readonly Currency CAD = new Currency("CAD", "Canadian Dollar", "$");
    public static readonly Currency CLP = new Currency("CLP", "Chilean Peso", "$");
    public static readonly Currency CNY = new Currency("CNY", "Chinese Yuan", "元");
    public static readonly Currency CZK = new Currency("CZK", "Czech Koruna", "Kč");
    public static readonly Currency DKK = new Currency("DKK", "Danish Krone", "kr");
    public static readonly Currency XCD = new Currency("XCD", "East Caribbean Dollar", "$");
    public static readonly Currency EGP = new Currency("EGP", "Egyptian Pound", "£");
    public static readonly Currency EUR = new Currency("EUR", "Euro", "€");
    public static readonly Currency HKD = new Currency("HKD", "Hong Kong Dollar", "元");
    public static readonly Currency HUF = new Currency("HUF", "Hungarian Forint", "Ft");
    public static readonly Currency ISK = new Currency("ISK", "Icelandic Krona", "kr");
    public static readonly Currency INR = new Currency("INR", "Indian Rupee", "₹");
    public static readonly Currency IDR = new Currency("IDR", "Indonesian Rupiah", "Rp");
    public static readonly Currency ILS = new Currency("ILS", "Israeli Sheqel", "₪");
    public static readonly Currency JMD = new Currency("JMD", "Jamaican Dollar", "J$");
    public static readonly Currency JPY = new Currency("JPY", "Japanese Yen", "¥");
    public static readonly Currency LBP = new Currency("LBP", "Lebanese Pound", "£");
    public static readonly Currency MYR = new Currency("MYR", "Malaysian Ringgit", "RM");
    public static readonly Currency MXN = new Currency("MXN", "Mexican Peso", "$");
    public static readonly Currency NAD = new Currency("NAD", "Namibian Dollar", "$");
    public static readonly Currency NPR = new Currency("NPR", "Nepalese Rupee", "₨");
    public static readonly Currency NZD = new Currency("NZD", "New Zealand Dollar", "$");
    public static readonly Currency NOK = new Currency("NOK", "Norwegian Krone", "kr");
    public static readonly Currency OMR = new Currency("OMR", "Omani Rial", "﷼");
    public static readonly Currency PKR = new Currency("PKR", "Pakistani Rupee", "₨");
    public static readonly Currency PAB = new Currency("PAB", "Panamanian Balboa", "B/.");
    public static readonly Currency PHP = new Currency("PHP", "Philippine Peso", "Ph");
    public static readonly Currency PLN = new Currency("PLN", "Polish Zloty", "zł");
    public static readonly Currency QAR = new Currency("QAR", "Qatari Riyal", "﷼");
    public static readonly Currency RON = new Currency("RON", "Romanian Leu", "le");
    public static readonly Currency RUB = new Currency("RUB", "Russian Rouble", "ру,;");
    public static readonly Currency SAR = new Currency("SAR", "Saudi Riyal", "﷼");
    public static readonly Currency SGD = new Currency("SGD", "Singapore Dollar", "$");
    public static readonly Currency ZAR = new Currency("ZAR", "South African Rand", "R");
    public static readonly Currency KRW = new Currency("KRW", "South Korean Won", "₩");
    public static readonly Currency LKR = new Currency("LKR", "Sri Lankan Rupee", "₨");
    public static readonly Currency SEK = new Currency("SEK", "Swedish Krona", "kr");
    public static readonly Currency CHF = new Currency("CHF", "Swiss Franc", "CHF");
    public static readonly Currency THB = new Currency("THB", "Thai Baht", "฿");
    public static readonly Currency TRY = new Currency("TRY", "Turkish Lira", "YTL");
    public static readonly Currency USD = new("USD", "US Dollar", "$");
}