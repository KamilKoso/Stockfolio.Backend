using System.Threading;
using System.Threading.Tasks;

namespace Stockfolio.Shared.Core.Events;

public interface IDomainEventHandler<in TEvent> where TEvent : class, IDomainEvent
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}