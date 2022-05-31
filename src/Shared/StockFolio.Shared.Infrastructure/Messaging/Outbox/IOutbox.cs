using System;
using System.Threading.Tasks;
using Stockfolio.Shared.Abstractions.Messaging;

namespace Stockfolio.Shared.Infrastructure.Messaging.Outbox;

public interface IOutbox
{
    bool Enabled { get; }
    Task SaveAsync(params IMessage[] messages);
    Task PublishUnsentAsync();
    Task CleanupAsync(DateTime? to = null);
}