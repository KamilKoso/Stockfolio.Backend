using System;

namespace Stockfolio.Shared.Abstractions.Kernel.Types;

public record AggregateId<T>(T Value);

public record AggregateId : AggregateId<Guid>
{
    public AggregateId() : this(Guid.NewGuid())
    {
    }

    public AggregateId(Guid value) : base(value)
    {
    }

    public static implicit operator Guid(AggregateId id) => id.Value;
    public static implicit operator AggregateId(Guid id) => new(id);

    public override string ToString() => Value.ToString();
}