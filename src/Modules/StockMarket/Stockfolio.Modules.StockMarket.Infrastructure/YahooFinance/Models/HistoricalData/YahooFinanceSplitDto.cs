using System.Text.Json.Serialization;

namespace Stockfolio.Modules.StockMarket.Infrastructure.YahooFinance.Models;

internal class YahooFinanceSplitDto
{
    [JsonPropertyName("date")]
    public long DateInSeconds { get; set; }

    public uint Numerator { get; set; }
    public uint Denominator { get; set; }
    public string SplitRatio { get; set; }
}