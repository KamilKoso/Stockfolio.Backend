using System.Text.Json.Serialization;

namespace Stockfolio.Modules.Assets.Infrastructure.YahooFinance.Models;

internal class YahooFinanceQuoteHistoricalData
{
    [JsonPropertyName("meta")]
    public YahooFinanceQuoteMetadata Metadata { get; set; }

    public YahooFinanceEvents Events { get; set; }

    [JsonPropertyName("timestamp")]
    public IReadOnlyList<long> TimestampsInSeconds { get; set; }

    public YahooFinanceIndicators Indicators { get; set; }
}