namespace Stockfolio.Modules.StockMarket.Application.Dto;

internal class DividendDto
{
    public DividendDto(DateTimeOffset date, decimal amount)
    {
        Date = date;
        Amount = amount;
    }

    public DateTimeOffset Date { get; set; }
    public decimal Amount { get; set; }
}