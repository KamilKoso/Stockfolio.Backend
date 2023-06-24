namespace Stockfolio.Modules.Assets.Core.Assets.Quotes;

internal class Quote
{
    public QuoteId Id { get; init; }
    public DateTimeOffset Date { get; private set; }
    public decimal Value { get; set; }
    public long? Volume { get; private set; }

    public Quote(DateTimeOffset date, decimal value, long? volume)
    {
        Id = Guid.NewGuid();
        Date = date;
        Value = value;
        Volume = volume;
    }
}