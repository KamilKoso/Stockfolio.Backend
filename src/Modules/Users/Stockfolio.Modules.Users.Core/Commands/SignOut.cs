using Stockfolio.Shared.Abstractions.Commands;

namespace Stockfolio.Modules.Users.Core.Commands;
internal record SignOut(Guid UserId) : ICommand;