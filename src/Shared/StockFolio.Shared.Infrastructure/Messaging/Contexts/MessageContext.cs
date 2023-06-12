using Stockfolio.Shared.Abstractions.Contexts;
using Stockfolio.Shared.Abstractions.Messaging;
using System;

namespace Stockfolio.Shared.Infrastructure.Messaging.Contexts;

public class MessageContext : IMessageContext
{
    public Guid MessageId { get; }
    public IContext Context { get; }

    public MessageContext(Guid messageId, IContext context)
    {
        MessageId = messageId;
        Context = context;
    }
}