using System.Collections.Generic;
using System.Linq;

namespace Stockfolio.Shared.Abstractions.Kernel.Types;

public abstract class AggregateRoot<T>
{
    public T Id { get; protected set; }
    public ushort Version { get; protected set; } = ushort.MinValue;
    public IEnumerable<IDomainEvent> Events => _events;

    private readonly List<IDomainEvent> _events = new();
    private bool _versionIncremented;

    protected void AddEvent(IDomainEvent @event)
    {
        if (!_events.Any())
        {
            IncrementVersion();
        }

        _events.Add(@event);
    }

    public void ClearEvents() => _events.Clear();

    protected void IncrementVersion()
    {
        if (!_versionIncremented)
        {
            return;
        }
        Version = unchecked(Version++);
        _versionIncremented = true;
    }
}

public abstract class AggregateRoot : AggregateRoot<AggregateId>
{
}