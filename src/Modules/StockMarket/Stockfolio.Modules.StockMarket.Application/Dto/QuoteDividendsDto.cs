namespace Stockfolio.Modules.StockMarket.Application.Dto;

internal class QuoteDividendsDto
{
    public string Symbol { get; set; }
    public string Currency { get; set; }
    public IEnumerable<DividendDto> Dividends { get; set; } = Enumerable.Empty<DividendDto>();
}