using System.Threading.Tasks;
using Stockfolio.Shared.Abstractions.Messaging;

namespace Stockfolio.Shared.Infrastructure.Messaging.Outbox;

public interface IOutboxBroker
{
    bool Enabled { get; }
    Task SendAsync(params IMessage[] messages);
}