using Stockfolio.Shared.Abstractions.Commands;

namespace Stockfolio.Modules.Users.Core.Commands;

internal record UpdateUserState(Guid UserId, string State) : ICommand;