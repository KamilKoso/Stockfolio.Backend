using System;

namespace Stockfolio.Shared.Abstractions.Kernel.Types;

public record UserId(Guid Value)
{
    public static implicit operator UserId(Guid id) => id.Equals(Guid.Empty) ? null : new(id);

    public static implicit operator Guid(UserId id) => id.Value;
}