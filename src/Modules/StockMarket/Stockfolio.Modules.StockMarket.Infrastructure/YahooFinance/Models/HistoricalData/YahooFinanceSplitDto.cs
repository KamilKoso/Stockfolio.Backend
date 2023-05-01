using System.Text.Json.Serialization;

namespace Stockfolio.Modules.StockMarket.Infrastructure.YahooFinance.Models;

internal class YahooFinanceSplitDto
{
    [JsonPropertyName("date")]
    public long DateInSeconds { get; set; }

    public decimal Numerator { get; set; }
    public decimal Denominator { get; set; }
    public string SplitRatio { get; set; }
}