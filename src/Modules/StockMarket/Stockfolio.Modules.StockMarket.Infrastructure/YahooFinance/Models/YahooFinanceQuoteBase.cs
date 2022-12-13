using System.Text.Json.Serialization;

namespace Stockfolio.Modules.StockMarket.Infrastructure.YahooFinance.Models;

internal class YahooFinanceQuoteBase
{
    public string Symbol { get; set; }
    public string LongName { get; set; }
    public string ShortName { get; set; }
    public string Exchange { get; set; }

    [JsonPropertyName("typeDisp")]
    public string Type { get; set; }

    public string QuoteType { get; set; }
}