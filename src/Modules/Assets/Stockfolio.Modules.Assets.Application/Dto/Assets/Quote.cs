namespace Stockfolio.Modules.Assets.Application.Dto.Assets;

internal record Quote
{
    public Quote(DateTimeOffset date, decimal? open, decimal? high, decimal? low, decimal? close, decimal? adjustedClose, long? volume)
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
    public decimal? Price { get => AdjustedClose ?? Close ?? Open; }
    public long? Volume { get; }
}