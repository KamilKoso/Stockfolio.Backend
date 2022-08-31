using System.Text.Json.Serialization;

namespace Stockfolio.Modules.StockMarket.Infrastructure.YahooFinance.Models;

internal class YahooFinanceQuoteDividend
{
    [JsonPropertyName("meta")]
    public YahooFinanceQuoteMetadata Metadata { get; set; }

    public YahooFinanceEvents Events { get; set; }
}

internal class YahooFinanceQuoteMetadata : YahooFinanceChart
{
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

    public IEnumerable<YahooFinanceTimePeriod> TradingPeriods { get; set; }
    public YahooFinanceIndicators Indicators { get; set; }

    internal class YahooFinanceCurrentTradingPeriod
    {
        [JsonPropertyName("pre")]
        public YahooFinanceTimePeriod PreMarket { get; set; }

        public YahooFinanceTimePeriod Regular { get; set; }

        [JsonPropertyName("post")]
        public YahooFinanceTimePeriod PostMarket { get; set; }
    }
}

internal class YahooFinanceEvents
{
    public Dictionary<long, YahooFinanceDividendDto> Dividends { get; set; }

    internal class YahooFinanceDividendDto
    {
        public decimal Amount { get; set; }

        [JsonPropertyName("date")]
        public long DateInSeconds { get; set; }
    }
}

internal class YahooFinanceIndicators
{
    public YahooFinanceQuote Quote { get; set; }

    [JsonPropertyName("adjclose")]
    public YahooFinanceAdjustedCloseWrapper AdjustedClose { get; set; }

    internal class YahooFinanceQuote
    {
        public IEnumerable<long> Volume { get; set; }
        public IEnumerable<decimal> Close { get; set; }
        public IEnumerable<decimal> Open { get; set; }
        public IEnumerable<decimal> High { get; set; }
        public IEnumerable<decimal> Low { get; set; }
    }
}

public class YahooFinanceAdjustedCloseWrapper
{
    public IEnumerable<YahooFinanceAdjustedClose> AdjustedClose { get; set; }

    public class YahooFinanceAdjustedClose
    {
        [JsonPropertyName("adjclose")]
        public IEnumerable<long> AdjustedClosePrices { get; set; }
    }
}