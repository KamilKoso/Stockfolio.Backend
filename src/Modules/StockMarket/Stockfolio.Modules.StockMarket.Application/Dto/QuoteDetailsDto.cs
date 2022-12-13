using Stockfolio.Modules.StockMarket.Application.DTO;

namespace Stockfolio.Modules.StockMarket.Application.Dto;

internal class QuoteDetailsDto : QuoteBaseDto
{
    public long MarketCap { get; set; }
    public string Language { get; set; }
    public string Region { get; set; }
    public string QuoteSourceName { get; set; }
    public DateTimeOffset FirstTradeDate { get; set; }
    public DateTimeOffset? LastDividendPayDate { get; set; }
    public long? Volume { get; set; }
    public decimal MarketOpenPrice { get; set; }
    public decimal Ask { get; set; }
    public decimal Bid { get; set; }
    public long? BidSize { get; set; }
    public string FullExchangeName { get; set; }
    public string FinancialCurrency { get; set; }
    public long AverageDailyVolume3Month { get; set; }
    public long AverageDailyVolume10Day { get; set; }

    public DateTimeOffset EarningsTime { get; set; }
    public DateTimeOffset EarningsTimeStart { get; set; }
    public DateTimeOffset EarningsTimeEnd { get; set; }
    public decimal? TrailingAnnualDividendRate { get; set; }
    public decimal? TrailingPE { get; set; }
    public decimal? EpsTrailingTwelveMonths { get; set; }
    public decimal? EpsForward { get; set; }
    public decimal BookValue { get; set; }

    public string FiftyTwoWeekRange { get; set; }
    public decimal? FiftyTwoWeekHigh { get; set; }
    public decimal? FiftyTwoWeekHighChange { get; set; }
    public decimal? FiftyTwoWeekHighChangePercent { get; set; }

    public decimal? FiftyTwoWeekLow { get; set; }
    public decimal? FiftyTwoWeekLowChange { get; set; }
    public decimal? FiftyTwoWeekLowChangePercent { get; set; }

    public decimal? FiftyDayAverage { get; set; }
    public decimal? FiftyDayAverageChange { get; set; }
    public decimal? FiftyDayAverageChangePercent { get; set; }

    public decimal? TwoHundredDayAverage { get; set; }
    public decimal? TwoHundredDayAverageChange { get; set; }
    public decimal? TwoHundredDayAverageChangePercent { get; set; }

    public string ExchangeTimezoneName { get; set; }
    public string ExchangeTimezoneShortName { get; set; }
    public string Market { get; set; }
    public decimal? EpsCurrentYear { get; set; }
    public decimal? PriceEpsCurrentYear { get; set; }
    public long SharesOutstanding { get; set; }
    public decimal? ForwardPE { get; set; }
    public decimal? PriceToBook { get; set; }
    public long ExchangeDataDelayedBy { get; set; }
}