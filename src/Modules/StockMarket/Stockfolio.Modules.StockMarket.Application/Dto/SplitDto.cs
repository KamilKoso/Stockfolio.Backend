namespace Stockfolio.Modules.StockMarket.Application.Dto;

internal record SplitDto
{
    public SplitDto(DateTimeOffset date, uint numerator, uint denominator)
    {
        Date = date;
        Numerator = numerator;
        Denominator = denominator;
    }

    public DateTimeOffset Date { get; }
    public uint Numerator { get; }
    public uint Denominator { get; }

    public string SplitRatio
    {
        get => $"{Numerator}:{Denominator}";
    }
}