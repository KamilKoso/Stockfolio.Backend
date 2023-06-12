namespace Stockfolio.Modules.Assets.Core.ValueObjects;
public record QuoteId(Guid Value)
{
    public static implicit operator QuoteId(Guid? id) => id is null || id == Guid.Empty ? null : new(id);

    public static implicit operator Guid(QuoteId id) => id.Value;
}