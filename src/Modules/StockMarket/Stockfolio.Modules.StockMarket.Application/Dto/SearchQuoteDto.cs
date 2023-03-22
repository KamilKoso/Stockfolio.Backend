using Stockfolio.Modules.StockMarket.Application.DTO;

namespace Stockfolio.Modules.StockMarket.Application.Dto;

internal record SearchQuoteDto : QuoteBaseDto
{
    public string ExchangeDisplayName { get; init; }
    public string Industry { get; init; }
    public string Sector { get; init; }
}