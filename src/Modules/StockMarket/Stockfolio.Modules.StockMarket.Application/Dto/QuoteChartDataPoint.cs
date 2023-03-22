namespace Stockfolio.Modules.StockMarket.Application.Dto;

internal record QuoteChartPricePoint
{
    public QuoteChartPricePoint(DateTimeOffset date, decimal? open, decimal? high, decimal? low, decimal? close, decimal? adjustedClose, long? volume)
    {
        Date = date;
        Open = open;
        High = high;
        Low = low;
        Close = close;
        AdjustedClose = adjustedClose;
        Volume = volume;
    }

    public DateTimeOffset Date { get; }
    public decimal? Open { get; }
    public decimal? High { get; }
    public decimal? Low { get; }
    public decimal? Close { get; }
    public decimal? AdjustedClose { get; }
    public long? Volume { get; }
}