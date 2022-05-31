using System;
using Stockfolio.Shared.Abstractions.Contexts;

namespace Stockfolio.Shared.Abstractions.Messaging;

public interface IMessageContext
{
    public Guid MessageId { get; }
    public IContext Context { get; }
}