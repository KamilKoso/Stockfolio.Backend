using Microsoft.Extensions.Logging;
using Stockfolio.Modules.Users.Core.Exceptions;
using Stockfolio.Modules.Users.Core.Managers;
using Stockfolio.Shared.Abstractions;
using Stockfolio.Shared.Abstractions.Commands;

namespace Stockfolio.Modules.Users.Core.Commands.Handlers;

internal sealed class ChangePasswordHandler : ICommandHandler<ChangePassword>
{
    private readonly UserManager _userManager;
    private readonly ILogger<ChangePasswordHandler> _logger;

    public ChangePasswordHandler(UserManager userManager,
                                 ILogger<ChangePasswordHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task HandleAsync(ChangePassword command, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(command.UserId.ToString())
                                        .NotNull(() => new UserNotFoundException(command.UserId));

        await _userManager.ChangePasswordAsync(user, command.CurrentPassword, command.NewPassword);
        _logger.LogInformation("User with ID: '{UserId}' has changed the password.", user.Id);
    }
}