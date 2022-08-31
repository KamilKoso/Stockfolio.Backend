using System.Text.Json.Serialization;

namespace Stockfolio.Modules.StockMarket.Infrastructure.YahooFinance.Models;

internal class YahooFinanceQuote
{
    public string Exchange { get; set; }
    public string ShortName { get; set; }
    public string LongName { get; set; }
    public string QuoteType { get; set; }
    public string Symbol { get; set; }
    public string Index { get; set; }
    public double Score { get; set; }

    [JsonPropertyName("typeDisp")]
    public string Type { get; set; }

    [JsonPropertyName("exchDisp")]
    public string ExchangeDisplayName { get; set; }

    public string Industry { get; set; }
    public string Sector { get; set; }
    public bool? IsYahooFinance { get; set; }
    public bool? DispSecIndFlag { get; set; } // no idea what that is
}