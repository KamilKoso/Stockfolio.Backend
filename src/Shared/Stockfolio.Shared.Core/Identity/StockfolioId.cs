using MassTransit;

namespace Stockfolio.Shared.Core.Identity;

public record StockfolioId<T>(T Value);

public record StockfolioId : StockfolioId<Guid>
{
    public static StockfolioId Next
        => NewId.NextSequentialGuid();

    public static StockfolioId Empty
        => NewId.Empty.ToSequentialGuid();

    public StockfolioId() : this(NewId.NextSequentialGuid())
    {
    }

    public StockfolioId(Guid value) : base(value)
    {
    }

    public static implicit operator Guid(StockfolioId id) => id.Value;
    public static implicit operator StockfolioId(Guid id) => new(id);

    public override string ToString() => Value.ToString();
}