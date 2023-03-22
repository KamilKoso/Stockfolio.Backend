using System.Text.Json.Serialization;

namespace Stockfolio.Modules.StockMarket.Infrastructure.YahooFinance.Models;

internal class YahooFinanceAdjustedClose
{
    [JsonPropertyName("adjclose")]
    public IReadOnlyList<decimal?> AdjustedClosePrices { get; set; }
}