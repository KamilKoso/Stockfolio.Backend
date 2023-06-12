using System;

namespace Stockfolio.Shared.Abstractions.Kernel.Types;

public record AssetId
{
    public Guid Value { get; init; }

    public AssetId(Guid value)
    {
        Value = value;
    }

    public AssetId()
    {
        Value = Guid.NewGuid();
    }

    public static implicit operator AssetId(Guid id) => id.Equals(Guid.Empty) ? null : new(id);

    public static implicit operator Guid(AssetId id) => id.Value;
}