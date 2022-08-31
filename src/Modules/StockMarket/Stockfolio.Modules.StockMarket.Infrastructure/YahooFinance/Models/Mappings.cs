using Stockfolio.Modules.StockMarket.Application.Dto;
using Stockfolio.Modules.StockMarket.Application.DTO;

namespace Stockfolio.Modules.StockMarket.Infrastructure.YahooFinance.Models;

internal static class Mappings
{
    public static QuoteDto AsQuoteDto(this YahooFinanceQuote yahooFinanceQuote)
        => yahooFinanceQuote.Map<QuoteDto>();

    public static QuoteDetailsDto AsQuoteDetailsDto(this YahooFinanceQuoteDetails yahooFinanceQuoteDetails)
    {
        var dto = yahooFinanceQuoteDetails.Map<QuoteDetailsDto>();
        dto.MarketCap = yahooFinanceQuoteDetails.MarketCap;
        dto.Market = yahooFinanceQuoteDetails.Market;
        dto.RegularMarketVolume = yahooFinanceQuoteDetails.RegularMarketVolume;
        dto.Region = yahooFinanceQuoteDetails.Region;
        dto.QuoteSourceName = yahooFinanceQuoteDetails.QuoteSourceName;
        dto.FirstTradeDate = DateTimeOffset.FromUnixTimeMilliseconds(yahooFinanceQuoteDetails.FirstTradeDateMilliseconds);
        dto.LastDividendPayDate = yahooFinanceQuoteDetails.DividendDate is null ? null : DateTimeOffset.FromUnixTimeSeconds((long)yahooFinanceQuoteDetails.DividendDate);
        dto.Currency = yahooFinanceQuoteDetails.Currency;
        dto.RegularMarketVolume = yahooFinanceQuoteDetails.RegularMarketVolume;
        dto.RegularMarketPreviousClose = yahooFinanceQuoteDetails.RegularMarketPreviousClose;
        dto.RegularMarketOpen = yahooFinanceQuoteDetails.RegularMarketOpen;
        dto.RegularMarketPrice = yahooFinanceQuoteDetails.RegularMarketPrice;
        dto.Ask = yahooFinanceQuoteDetails.Ask;
        dto.Bid = yahooFinanceQuoteDetails.Bid;
        dto.BidSize = yahooFinanceQuoteDetails.BidSize;
        dto.FullExchangeName = yahooFinanceQuoteDetails.FullExchangeName;
        dto.FinancialCurrency = yahooFinanceQuoteDetails.FinancialCurrency;
        dto.AverageDailyVolume3Month = yahooFinanceQuoteDetails.AverageDailyVolume3Month;
        dto.AverageDailyVolume10Day = yahooFinanceQuoteDetails.AverageDailyVolume10Day;
        dto.EarningsTime = DateTimeOffset.FromUnixTimeSeconds(yahooFinanceQuoteDetails.EarningsTimestamp);
        dto.EarningsTimeStart = DateTimeOffset.FromUnixTimeSeconds(yahooFinanceQuoteDetails.EarningsTimestampStart);
        dto.EarningsTimeEnd = DateTimeOffset.FromUnixTimeSeconds(yahooFinanceQuoteDetails.EarningsTimestampEnd);
        dto.TrailingAnnualDividendRate = yahooFinanceQuoteDetails.TrailingAnnualDividendRate;
        dto.TrailingPE = yahooFinanceQuoteDetails.TrailingPE;
        dto.EpsTrailingTwelveMonths = yahooFinanceQuoteDetails.EpsTrailingTwelveMonths;
        dto.EpsForward = yahooFinanceQuoteDetails.EpsForward;
        dto.BookValue = yahooFinanceQuoteDetails.BookValue;
        dto.FiftyTwoWeekRange = yahooFinanceQuoteDetails.FiftyTwoWeekRange;
        dto.FiftyTwoWeekHigh = yahooFinanceQuoteDetails.FiftyTwoWeekHigh;
        dto.FiftyDayAverageChangePercent = yahooFinanceQuoteDetails.FiftyDayAverageChangePercent;
        dto.FiftyTwoWeekLow = yahooFinanceQuoteDetails.FiftyTwoWeekLow;
        dto.FiftyTwoWeekLowChange = yahooFinanceQuoteDetails.FiftyTwoWeekLowChange;
        dto.FiftyTwoWeekLowChangePercent = yahooFinanceQuoteDetails.FiftyTwoWeekLowChangePercent;
        dto.FiftyDayAverage = yahooFinanceQuoteDetails.FiftyDayAverage;
        dto.FiftyDayAverageChange = yahooFinanceQuoteDetails.FiftyDayAverageChange;
        dto.FiftyDayAverageChangePercent = yahooFinanceQuoteDetails.FiftyDayAverageChangePercent;
        dto.TwoHundredDayAverage = yahooFinanceQuoteDetails.TwoHundredDayAverage;
        dto.TwoHundredDayAverageChange = yahooFinanceQuoteDetails.TwoHundredDayAverageChange;
        dto.TwoHundredDayAverageChangePercent = yahooFinanceQuoteDetails.TwoHundredDayAverageChangePercent;
        dto.ExchangeTimezoneName = yahooFinanceQuoteDetails.ExchangeTimezoneName;
        dto.ExchangeTimezoneShortName = yahooFinanceQuoteDetails.ExchangeTimezoneShortName;
        dto.EpsCurrentYear = yahooFinanceQuoteDetails.EpsCurrentYear;
        dto.PriceEpsCurrentYear = yahooFinanceQuoteDetails.PriceEpsCurrentYear;
        dto.SharesOutstanding = yahooFinanceQuoteDetails.SharesOutstanding;
        dto.MarketCap = yahooFinanceQuoteDetails.MarketCap;
        dto.ForwardPE = yahooFinanceQuoteDetails.ForwardPE;
        dto.PriceToBook = yahooFinanceQuoteDetails.PriceToBook;
        dto.ExchangeDataDelayedBy = yahooFinanceQuoteDetails.ExchangeDataDelayedBy;
        return dto;
    }

    public static QuoteDividendsDto AsQuoteDividendsDto(this YahooFinanceQuoteDividend yahooFinanceQuoteDetails)
    {
        return new QuoteDividendsDto
        {
            Currency = yahooFinanceQuoteDetails.Metadata.Currency,
            Symbol = yahooFinanceQuoteDetails.Metadata.Symbol,
            Dividends = yahooFinanceQuoteDetails.Events.Dividends.Select(x => new DividendDto(
                                                                                    DateTimeOffset
                                                                                        .FromUnixTimeSeconds(x.Key)
                                                                                        .ToOffset(TimeSpan.FromSeconds(yahooFinanceQuoteDetails.Metadata.GmtOffsetInSeconds)),
                                                                                    x.Value.Amount))
        };
    }

    private static T Map<T>(this YahooFinanceQuote quote) where T : QuoteDto, new()
        => new()
        {
            Exchange = quote.Exchange,
            ExchangeDisplayName = quote.ExchangeDisplayName,
            Industry = quote.Industry,
            Name = quote.LongName,
            ShortName = quote.ShortName,
            Sector = quote.Sector,
            Symbol = quote.Symbol
        };
}