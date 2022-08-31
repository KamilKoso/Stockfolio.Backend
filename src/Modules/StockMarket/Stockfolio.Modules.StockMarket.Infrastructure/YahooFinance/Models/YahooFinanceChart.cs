using System.Text.Json.Serialization;

namespace Stockfolio.Modules.StockMarket.Infrastructure.YahooFinance.Models;

public class YahooFinanceChart
{
    public string DataGranularity { get; set; }
    public string Range { get; set; }
    public IEnumerable<string> ValidRanges { get; set; }

    [JsonPropertyName("timestamp")]
    public IEnumerable<long> Timestamps { get; set; }
}