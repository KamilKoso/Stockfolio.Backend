namespace Stockfolio.Modules.Assets.Application.Dto.Assets;

internal record Quote
{
    public Quote(DateTimeOffset date, decimal? value, long? volume)
    {
        Date = date;
        Value = value;
        Volume = volume;
    }

    public DateTimeOffset Date { get; }
    public decimal? Value { get; set; }
    public long? Volume { get; }
}