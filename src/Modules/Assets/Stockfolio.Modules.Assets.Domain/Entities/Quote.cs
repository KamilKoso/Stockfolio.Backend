using Stockfolio.Modules.Assets.Core.ValueObjects;

namespace Stockfolio.Modules.Assets.Core.Entities;

internal class Quote
{
    public QuoteId Id { get; init; }
    public DateTimeOffset Date { get; private set; }
    public decimal Value { get; internal set; }
    public long? Volume { get; private set; }

    public Quote(DateTimeOffset date, decimal value, long? volume)
    {
        Id = Guid.NewGuid();
        Date = date;
        Value = value;
        Volume = volume;
    }
}