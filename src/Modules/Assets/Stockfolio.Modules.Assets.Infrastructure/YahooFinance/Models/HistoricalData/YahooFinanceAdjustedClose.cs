using System.Text.Json.Serialization;

namespace Stockfolio.Modules.Assets.Infrastructure.YahooFinance.Models.HistoricalData;

internal class YahooFinanceAdjustedClose
{
    [JsonPropertyName("adjclose")]
    public IReadOnlyList<decimal?> AdjustedClosePrices { get; set; }
}