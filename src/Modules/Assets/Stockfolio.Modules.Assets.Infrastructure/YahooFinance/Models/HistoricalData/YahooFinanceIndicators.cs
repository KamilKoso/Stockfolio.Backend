using System.Text.Json.Serialization;

namespace Stockfolio.Modules.Assets.Infrastructure.YahooFinance.Models;

internal class YahooFinanceIndicators
{
    public IReadOnlyList<YahooFinanceQuote> Quote { get; set; }

    [JsonPropertyName("adjclose")]
    public IReadOnlyList<YahooFinanceAdjustedClose> AdjustedClose { get; set; }

    internal class YahooFinanceQuote
    {
        public IReadOnlyList<long?> Volume { get; set; }
        public IReadOnlyList<decimal?> Close { get; set; }
        public IReadOnlyList<decimal?> Open { get; set; }
        public IReadOnlyList<decimal?> High { get; set; }
        public IReadOnlyList<decimal?> Low { get; set; }
    }
}