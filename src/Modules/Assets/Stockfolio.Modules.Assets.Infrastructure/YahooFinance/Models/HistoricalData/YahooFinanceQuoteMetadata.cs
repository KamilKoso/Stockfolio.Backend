using Stockfolio.Modules.Assets.Infrastructure.YahooFinance.Models.HistoricalData;
using System.Text.Json.Serialization;

namespace Stockfolio.Modules.Assets.Infrastructure.YahooFinance.Models;

internal class YahooFinanceQuoteMetadata
{
    public string DataGranularity { get; set; }
    public string Range { get; set; }
    public IReadOnlyList<string> ValidRanges { get; set; }
    public string Currency { get; set; }
    public string Symbol { get; set; }
    public string ExchangeName { get; set; }
    public string InstrumentType { get; set; }

    [JsonPropertyName("firstTradeDate")]
    public long FirstTradeDateMiliseconds { get; set; }

    [JsonPropertyName("regularMarketTime")]
    public long RegularMarketTimeMiliseconds { get; set; }

    [JsonPropertyName("gmtoffset")]
    public int GmtOffsetInSeconds { get; set; }

    public string Timezone { get; set; }
    public string ExchangeTimezoneName { get; set; }
    public decimal RegularMarketPrice { get; set; }
    public decimal ChartPreviousClose { get; set; }
    public decimal PreviousClose { get; set; }
    public int Scale { get; set; }

    [JsonPropertyName("currentTradingPeriod")]
    public YahooFinanceCurrentTradingPeriod CurrentTradingPeriods { get; set; }

    public IReadOnlyList<IReadOnlyList<YahooFinanceTimePeriod>> TradingPeriods { get; set; }
}