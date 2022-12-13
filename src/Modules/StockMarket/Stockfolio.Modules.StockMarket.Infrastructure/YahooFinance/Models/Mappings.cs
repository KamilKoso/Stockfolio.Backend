using Stockfolio.Modules.StockMarket.Application.Dto;
using Stockfolio.Modules.StockMarket.Application.DTO;

namespace Stockfolio.Modules.StockMarket.Infrastructure.YahooFinance.Models;

internal static class Mappings
{
    public static SearchQuoteDto AsSearchQuoteDto(this YahooFinanceQuoteDetails yahooFinanceQuoteDetails, SearchYahooFinanceQuote searchYahooFinanceQuote)
    {
        var dto = yahooFinanceQuoteDetails.Map<SearchQuoteDto>();
        dto.ExchangeDisplayName = searchYahooFinanceQuote.ExchangeDisplayName;
        dto.Industry = searchYahooFinanceQuote.Industry;
        dto.Sector = searchYahooFinanceQuote.Sector;
        return dto;
    }

    public static QuoteDetailsDto AsQuoteDetailsDto(this YahooFinanceQuoteDetails quote)
    {
        var dto = quote.Map<QuoteDetailsDto>();
        dto.MarketCap = quote.MarketCap;
        dto.Market = quote.Market;
        dto.Volume = quote.RegularMarketVolume;
        dto.Region = quote.Region;
        dto.QuoteSourceName = quote.QuoteSourceName;
        dto.FirstTradeDate = DateTimeOffset.FromUnixTimeMilliseconds(quote.FirstTradeDateMilliseconds);
        dto.LastDividendPayDate = quote.DividendDate is null ? null : DateTimeOffset.FromUnixTimeSeconds((long)quote.DividendDate);
        dto.Ask = quote.Ask;
        dto.Bid = quote.Bid;
        dto.BidSize = quote.BidSize;
        dto.FullExchangeName = quote.FullExchangeName;
        dto.FinancialCurrency = quote.FinancialCurrency;
        dto.AverageDailyVolume3Month = quote.AverageDailyVolume3Month;
        dto.AverageDailyVolume10Day = quote.AverageDailyVolume10Day;
        dto.EarningsTime = DateTimeOffset.FromUnixTimeSeconds(quote.EarningsTimestamp);
        dto.EarningsTimeStart = DateTimeOffset.FromUnixTimeSeconds(quote.EarningsTimestampStart);
        dto.EarningsTimeEnd = DateTimeOffset.FromUnixTimeSeconds(quote.EarningsTimestampEnd);
        dto.TrailingAnnualDividendRate = quote.TrailingAnnualDividendRate;
        dto.TrailingPE = quote.TrailingPE;
        dto.EpsTrailingTwelveMonths = quote.EpsTrailingTwelveMonths;
        dto.EpsForward = quote.EpsForward;
        dto.BookValue = quote.BookValue;
        dto.FiftyTwoWeekRange = quote.FiftyTwoWeekRange;
        dto.FiftyTwoWeekHigh = quote.FiftyTwoWeekHigh;
        dto.FiftyDayAverageChangePercent = quote.FiftyDayAverageChangePercent;
        dto.FiftyTwoWeekLow = quote.FiftyTwoWeekLow;
        dto.FiftyTwoWeekLowChange = quote.FiftyTwoWeekLowChange;
        dto.FiftyTwoWeekLowChangePercent = quote.FiftyTwoWeekLowChangePercent;
        dto.FiftyDayAverage = quote.FiftyDayAverage;
        dto.FiftyDayAverageChange = quote.FiftyDayAverageChange;
        dto.TwoHundredDayAverage = quote.TwoHundredDayAverage;
        dto.TwoHundredDayAverageChange = quote.TwoHundredDayAverageChange;
        dto.TwoHundredDayAverageChangePercent = quote.TwoHundredDayAverageChangePercent;
        dto.ExchangeTimezoneName = quote.ExchangeTimezoneName;
        dto.ExchangeTimezoneShortName = quote.ExchangeTimezoneShortName;
        dto.EpsCurrentYear = quote.EpsCurrentYear;
        dto.PriceEpsCurrentYear = quote.PriceEpsCurrentYear;
        dto.SharesOutstanding = quote.SharesOutstanding;
        dto.ForwardPE = quote.ForwardPE;
        dto.PriceToBook = quote.PriceToBook;
        dto.ExchangeDataDelayedBy = quote.ExchangeDataDelayedBy;
        dto.MarketOpenPrice = quote.RegularMarketPreviousClose;
        return dto;
    }

    public static QuoteDividendsDto AsQuoteDividendsDto(this YahooFinanceQuoteDividend yahooFinanceQuoteDividend)
    {
        return new QuoteDividendsDto
        {
            Currency = yahooFinanceQuoteDividend.Metadata.Currency,
            Symbol = yahooFinanceQuoteDividend.Metadata.Symbol,
            Dividends = yahooFinanceQuoteDividend.Events.Dividends.Select(x => new DividendDto(
                                                                                    DateTimeOffset
                                                                                        .FromUnixTimeSeconds(x.Key)
                                                                                        .ToOffset(TimeSpan.FromSeconds(yahooFinanceQuoteDividend.Metadata.GmtOffsetInSeconds)),
                                                                                    x.Value.Amount))
        };
    }

    private static T Map<T>(this YahooFinanceQuoteDetails quote) where T : QuoteBaseDto, new()
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