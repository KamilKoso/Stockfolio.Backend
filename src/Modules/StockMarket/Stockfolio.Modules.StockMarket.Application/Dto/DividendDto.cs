namespace Stockfolio.Modules.StockMarket.Application.Dto;

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