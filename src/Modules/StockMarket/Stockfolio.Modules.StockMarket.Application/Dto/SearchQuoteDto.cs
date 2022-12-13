using Stockfolio.Modules.StockMarket.Application.DTO;

namespace Stockfolio.Modules.StockMarket.Application.Dto;

internal class SearchQuoteDto : QuoteBaseDto
{
    public string ExchangeDisplayName { get; set; }
    public string Industry { get; set; }
    public string Sector { get; set; }
}