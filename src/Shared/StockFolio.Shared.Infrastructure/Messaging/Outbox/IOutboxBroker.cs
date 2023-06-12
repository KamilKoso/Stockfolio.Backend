using Stockfolio.Shared.Abstractions.Messaging;
using System.Threading.Tasks;

namespace Stockfolio.Shared.Infrastructure.Messaging.Outbox;

public interface IOutboxBroker
{
    bool Enabled { get; }

    Task SendAsync(params IMessage[] messages);
}