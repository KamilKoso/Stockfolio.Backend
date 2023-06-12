namespace Stockfolio.Modules.Assets.Application.Dto.Assets;

internal record DividendDto
{
    public DividendDto(DateTimeOffset date, decimal amount)
    {
        Date = date;
        Amount = amount;
    }

    public DateTimeOffset Date { get; init; }
    public decimal Amount { get; init; }
}