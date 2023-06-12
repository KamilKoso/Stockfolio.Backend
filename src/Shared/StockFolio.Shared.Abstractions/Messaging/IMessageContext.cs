using Stockfolio.Shared.Abstractions.Contexts;
using System;

namespace Stockfolio.Shared.Abstractions.Messaging;

public interface IMessageContext
{
    public Guid MessageId { get; }
    public IContext Context { get; }
}