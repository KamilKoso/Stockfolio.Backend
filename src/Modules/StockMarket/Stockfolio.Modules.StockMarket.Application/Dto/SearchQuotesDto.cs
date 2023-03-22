using Stockfolio.Modules.StockMarket.Application.Dto;

namespace Stockfolio.Modules.StockMarket.Application.DTO;

internal record SearchQuotesDto
{
    public SearchQuotesDto(IList<SearchQuoteDto> quotes)
    {
        Quotes = (quotes ?? Array.Empty<SearchQuoteDto>()).AsReadOnly();
    }

    public IReadOnlyCollection<SearchQuoteDto> Quotes { get; }
    public int Count { get => Quotes.Count; }
}