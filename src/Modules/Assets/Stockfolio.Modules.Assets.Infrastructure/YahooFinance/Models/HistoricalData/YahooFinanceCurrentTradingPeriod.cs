using System.Text.Json.Serialization;

namespace Stockfolio.Modules.Assets.Infrastructure.YahooFinance.Models.HistoricalData;

internal class YahooFinanceCurrentTradingPeriod
{
    [JsonPropertyName("pre")]
    public YahooFinanceTimePeriod PreMarket { get; set; }

    public YahooFinanceTimePeriod Regular { get; set; }

    [JsonPropertyName("post")]
    public YahooFinanceTimePeriod PostMarket { get; set; }
}