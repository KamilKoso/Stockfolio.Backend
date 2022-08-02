using Stockfolio.Modules.StockMarket.Application.Dto;
using Stockfolio.Modules.StockMarket.Application.DTO;

namespace Stockfolio.Modules.StockMarket.Infrastructure.YahooFinance.Dto.Mappings;

internal static class Mappings
{
    public static QuoteDto AsQuoteDto(this YahooFinanceQuoteDto yahooFinanceQuote)
        => yahooFinanceQuote.Map<QuoteDto>();

    public static QuoteDetailsDto AsDetailsQuoteDto(this YahooFinanceQuoteDetailsDto yahooFinanceQuoteDetails)
    {
        var dto = yahooFinanceQuoteDetails.Map<QuoteDetailsDto>();
        return dto;
    }

    public static T Map<T>(this YahooFinanceQuoteDto quote) where T : QuoteDto, new()
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