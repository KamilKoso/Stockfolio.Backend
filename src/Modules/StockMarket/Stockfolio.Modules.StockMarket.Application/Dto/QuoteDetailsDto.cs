using Stockfolio.Modules.StockMarket.Application.DTO;

namespace Stockfolio.Modules.StockMarket.Application.Dto;

internal record QuoteDetailsDto : QuoteBaseDto
{
    public long MarketCap { get; init; }
    public string Language { get; init; }
    public string Region { get; init; }
    public string QuoteSourceName { get; init; }
    public DateTimeOffset FirstTradeDate { get; init; }
    public DateTimeOffset? LastDividendPayDate { get; init; }
    public long? Volume { get; init; }
    public decimal MarketOpenPrice { get; init; }
    public decimal Ask { get; init; }
    public decimal Bid { get; init; }
    public long? BidSize { get; init; }
    public string FullExchangeName { get; init; }
    public string FinancialCurrency { get; init; }
    public long AverageDailyVolume3Month { get; init; }
    public long AverageDailyVolume10Day { get; init; }

    public DateTimeOffset EarningsTime { get; init; }
    public DateTimeOffset EarningsTimeStart { get; init; }
    public DateTimeOffset EarningsTimeEnd { get; init; }
    public decimal? TrailingAnnualDividendRate { get; init; }
    public decimal? TrailingPE { get; init; }
    public decimal? EpsTrailingTwelveMonths { get; init; }
    public decimal? EpsForward { get; init; }
    public decimal BookValue { get; init; }

    public string FiftyTwoWeekRange { get; init; }
    public decimal? FiftyTwoWeekHigh { get; init; }
    public decimal? FiftyTwoWeekHighChange { get; init; }
    public decimal? FiftyTwoWeekHighChangePercent { get; init; }

    public decimal? FiftyTwoWeekLow { get; init; }
    public decimal? FiftyTwoWeekLowChange { get; init; }
    public decimal? FiftyTwoWeekLowChangePercent { get; init; }

    public decimal? FiftyDayAverage { get; init; }
    public decimal? FiftyDayAverageChange { get; init; }
    public decimal? FiftyDayAverageChangePercent { get; init; }

    public decimal? TwoHundredDayAverage { get; init; }
    public decimal? TwoHundredDayAverageChange { get; init; }
    public decimal? TwoHundredDayAverageChangePercent { get; init; }

    public string ExchangeTimezoneName { get; init; }
    public string ExchangeTimezoneShortName { get; init; }
    public string Market { get; init; }
    public decimal? EpsCurrentYear { get; init; }
    public decimal? PriceEpsCurrentYear { get; init; }
    public long SharesOutstanding { get; init; }
    public decimal? ForwardPE { get; init; }
    public decimal? PriceToBook { get; init; }
    public long ExchangeDataDelayedBy { get; init; }
}