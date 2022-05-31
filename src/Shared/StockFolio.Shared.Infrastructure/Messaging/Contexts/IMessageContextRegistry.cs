using Stockfolio.Shared.Abstractions.Messaging;

namespace Stockfolio.Shared.Infrastructure.Messaging.Contexts;

public interface IMessageContextRegistry
{
    void Set(IMessage message, IMessageContext context);
}