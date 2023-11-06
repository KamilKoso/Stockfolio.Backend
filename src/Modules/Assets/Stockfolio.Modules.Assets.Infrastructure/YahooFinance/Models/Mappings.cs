using Stockfolio.Modules.Assets.Application.Dto.Assets;
using Stockfolio.Modules.Assets.Infrastructure.YahooFinance.Models.HistoricalData;

namespace Stockfolio.Modules.Assets.Infrastructure.YahooFinance.Models;

internal static class Mappings
{
    public static SearchSecurityDto AsSearchQuoteDto(this YahooFinanceSecurityDetails yahooFinanceSecurityDetails, SearchYahooFinanceSecurities searchYahooFinanceSecurities)
    {
        return yahooFinanceSecurityDetails.Map<SearchSecurityDto>() with
        {
            ExchangeDisplayName = searchYahooFinanceSecurities.ExchangeDisplayName,
            Industry = searchYahooFinanceSecurities.Industry,
            Sector = searchYahooFinanceSecurities.Sector,
        };
    }

    public static AssetDto AsQuoteDetailsDto(this YahooFinanceSecurityDetails quote)
    {
        return quote.Map<AssetDto>() with
        {
            MarketCap = quote.MarketCap,
            Market = quote.Market,
            Volume = quote.RegularMarketVolume,
            Region = quote.Region,
            QuoteSourceName = quote.QuoteSourceName,
            FirstTradeDate = DateTimeOffset.FromUnixTimeMilliseconds(quote.FirstTradeDateMilliseconds),
            LastDividendPayDate = quote.DividendDate is null ? null : DateTimeOffset.FromUnixTimeSeconds((long)quote.DividendDate),
            Ask = quote.Ask,
            Bid = quote.Bid,
            BidSize = quote.BidSize,
            FullExchangeName = quote.FullExchangeName,
            FinancialCurrency = quote.FinancialCurrency,
            AverageDailyVolume3Month = quote.AverageDailyVolume3Month,
            AverageDailyVolume10Day = quote.AverageDailyVolume10Day,
            EarningsTime = DateTimeOffset.FromUnixTimeSeconds(quote.EarningsTimestamp),
            EarningsTimeStart = DateTimeOffset.FromUnixTimeSeconds(quote.EarningsTimestampStart),
            EarningsTimeEnd = DateTimeOffset.FromUnixTimeSeconds(quote.EarningsTimestampEnd),
            TrailingAnnualDividendRate = quote.TrailingAnnualDividendRate,
            TrailingPE = quote.TrailingPE,
            EpsTrailingTwelveMonths = quote.EpsTrailingTwelveMonths,
            EpsForward = quote.EpsForward,
            BookValue = quote.BookValue,
            FiftyTwoWeekRange = quote.FiftyTwoWeekRange,
            FiftyTwoWeekHigh = quote.FiftyTwoWeekHigh,
            FiftyDayAverageChangePercent = quote.FiftyDayAverageChangePercent,
            FiftyTwoWeekLow = quote.FiftyTwoWeekLow,
            FiftyTwoWeekLowChange = quote.FiftyTwoWeekLowChange,
            FiftyTwoWeekLowChangePercent = quote.FiftyTwoWeekLowChangePercent,
            FiftyDayAverage = quote.FiftyDayAverage,
            FiftyDayAverageChange = quote.FiftyDayAverageChange,
            TwoHundredDayAverage = quote.TwoHundredDayAverage,
            TwoHundredDayAverageChange = quote.TwoHundredDayAverageChange,
            TwoHundredDayAverageChangePercent = quote.TwoHundredDayAverageChangePercent,
            ExchangeTimezoneName = quote.ExchangeTimezoneName,
            ExchangeTimezoneShortName = quote.ExchangeTimezoneShortName,
            EpsCurrentYear = quote.EpsCurrentYear,
            PriceEpsCurrentYear = quote.PriceEpsCurrentYear,
            SharesOutstanding = quote.SharesOutstanding,
            ForwardPE = quote.ForwardPE,
            PriceToBook = quote.PriceToBook,
            ExchangeDataDelayedBy = quote.ExchangeDataDelayedBy,
            MarketOpenPrice = quote.RegularMarketPreviousClose,
        };
    }

    public static HistoricalQuotesDto AsQuoteHistoricalData(this YahooFinanceQuoteHistoricalData yahooFinanceQuoteDividend)
    {
        var dividends = yahooFinanceQuoteDividend.Events?.Dividends?
                         .Select(x => new DividendDto(DateTimeOffset.FromUnixTimeSeconds(x.Key).ToOffset(TimeSpan.FromSeconds(yahooFinanceQuoteDividend.Metadata.GmtOffsetInSeconds)),
                                                      x.Value.Amount))
                         .ToList();
        var splits = yahooFinanceQuoteDividend.Events?.Splits?
                         .Select(x => new SplitDto(DateTimeOffset.FromUnixTimeSeconds(x.Key).ToOffset(TimeSpan.FromSeconds(yahooFinanceQuoteDividend.Metadata.GmtOffsetInSeconds)),
                                                   x.Value.Numerator,
                                                   x.Value.Denominator))
                         .ToList();

        var historicalQuotes = yahooFinanceQuoteDividend.TimestampsInSeconds?
            .Select((value, index) =>
            {
                var date = DateTimeOffset
                                .FromUnixTimeSeconds(yahooFinanceQuoteDividend.TimestampsInSeconds[index])
                                .ToOffset(TimeSpan.FromSeconds(yahooFinanceQuoteDividend.Metadata.GmtOffsetInSeconds));

                var open = yahooFinanceQuoteDividend.Indicators.Quote[0].Open[index];
                var high = yahooFinanceQuoteDividend.Indicators.Quote[0].High[index];
                var low = yahooFinanceQuoteDividend.Indicators.Quote[0].Low[index];
                var close = yahooFinanceQuoteDividend.Indicators.Quote[0].Close[index];
                var adjustedClose = yahooFinanceQuoteDividend.Indicators.AdjustedClose?[0].AdjustedClosePrices[index];
                var volume = yahooFinanceQuoteDividend.Indicators.Quote[0].Volume[index];
                return new Quote(date, open, high, low, close, adjustedClose, volume);
            }).ToList();

        return new(symbol: yahooFinanceQuoteDividend.Metadata.Symbol,
                   currency: yahooFinanceQuoteDividend.Metadata.Currency,
                   dividends: dividends,
                   splits: splits,
                   quotes: historicalQuotes);
    }

    private static T Map<T>(this YahooFinanceSecurityDetails quote) where T : SecurityBaseDto, new()
        => new()
        {
            Exchange = quote.Exchange,
            Name = quote.LongName,
            ShortName = quote.ShortName,
            Symbol = quote.Symbol,
            Price = quote.RegularMarketPrice,
            PreviousClosePrice = quote.RegularMarketPreviousClose,
            Currency = quote.Currency
        };
}