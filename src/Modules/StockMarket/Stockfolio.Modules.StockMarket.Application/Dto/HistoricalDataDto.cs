namespace Stockfolio.Modules.StockMarket.Application.Dto;

internal record HistoricalDataDto
{
    public HistoricalDataDto(string symbol, string currency, IList<DividendDto> dividends, IList<SplitDto> splits, IList<QuoteChartPricePoint> prices)
    {
        Symbol = symbol;
        Currency = currency;
        Dividends = (dividends ?? Array.Empty<DividendDto>()).AsReadOnly();
        Splits = (splits ?? Array.Empty<SplitDto>()).AsReadOnly();
        Prices = (prices ?? Array.Empty<QuoteChartPricePoint>()).AsReadOnly();
    }

    public string Symbol { get; private set; }
    public string Currency { get; private set; }
    public IReadOnlyCollection<DividendDto> Dividends { get; private set; }
    public IReadOnlyCollection<SplitDto> Splits { get; private set; }
    public IReadOnlyCollection<QuoteChartPricePoint> Prices { get; private set; }
}