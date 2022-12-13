namespace Stockfolio.Modules.StockMarket.Application.DTO;

internal abstract class QuoteBaseDto
{
    public string Exchange { get; set; }
    public string ShortName { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public decimal Price { get; set; }
    public decimal PreviousClosePrice { get; set; }
    public string Currency { get; set; }
}