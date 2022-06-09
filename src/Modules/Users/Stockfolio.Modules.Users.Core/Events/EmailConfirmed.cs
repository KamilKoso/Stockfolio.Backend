using Stockfolio.Shared.Abstractions.Events;

namespace Stockfolio.Modules.Users.Core.Events;
internal record EmailConfirmed(Guid UserId) : IEvent;