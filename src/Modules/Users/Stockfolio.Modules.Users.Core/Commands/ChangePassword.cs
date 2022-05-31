using Stockfolio.Shared.Abstractions.Commands;

namespace Stockfolio.Modules.Users.Core.Commands;
internal record ChangePassword(Guid UserId, string CurrentPassword, string NewPassword) : ICommand;