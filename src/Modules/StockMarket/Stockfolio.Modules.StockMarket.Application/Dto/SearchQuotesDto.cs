namespace Stockfolio.Modules.StockMarket.Application.DTO;

internal class SearchQuotesDto
{
    public IEnumerable<QuoteDto> Quotes { get; set; } = Array.Empty<QuoteDto>();
    public int Count { get => Quotes.Count(); }
}