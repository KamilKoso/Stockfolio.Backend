using Stockfolio.Modules.StockMarket.Application.DTO;

namespace Stockfolio.Modules.StockMarket.Application.Dto;

internal class QuoteDetailsDto : QuoteDto
{
    public long MarketCap { get; set; }
    public long Volume { get; set; }
}