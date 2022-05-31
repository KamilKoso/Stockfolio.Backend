using Stockfolio.Shared.Abstractions.Events;

namespace Stockfolio.Modules.Users.Core.Events;
internal record SignedOut(Guid UserId) : IEvent;