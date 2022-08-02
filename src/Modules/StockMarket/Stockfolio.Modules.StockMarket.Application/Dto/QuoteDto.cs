namespace Stockfolio.Modules.StockMarket.Application.DTO;

internal class QuoteDto
{
    public string Exchange { get; set; }
    public string ShortName { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public string ExchangeDisplayName { get; set; }
    public string Industry { get; set; }
    public string Sector { get; set; }
}