﻿namespace Stockfolio.Modules.StockMarket.Application.DTO;

internal abstract record QuoteBaseDto
{
    public string Exchange { get; init; }
    public string ShortName { get; init; }
    public string Name { get; init; }
    public string Symbol { get; init; }
    public decimal Price { get; init; }
    public decimal PreviousClosePrice { get; init; }
    public string Currency { get; init; }
}