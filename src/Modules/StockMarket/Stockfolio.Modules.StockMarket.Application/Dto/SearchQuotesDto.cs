﻿using Stockfolio.Modules.StockMarket.Application.Dto;

namespace Stockfolio.Modules.StockMarket.Application.DTO;

internal class SearchQuotesDto
{
    public IList<SearchQuoteDto> Quotes { get; set; } = Array.Empty<SearchQuoteDto>();
    public int Count { get => Quotes.Count; }
}