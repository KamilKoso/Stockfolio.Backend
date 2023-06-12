namespace Stockfolio.Modules.Assets.Application.Dto.Assets;

internal record SplitDto
{
    public SplitDto(DateTimeOffset date, decimal numerator, decimal denominator)
    {
        Date = date;
        Numerator = numerator;
        Denominator = denominator;
    }

    public DateTimeOffset Date { get; }
    public decimal Numerator { get; }
    public decimal Denominator { get; }

    public string SplitRatio
    {
        get => $"{Numerator}:{Denominator}";
    }
}