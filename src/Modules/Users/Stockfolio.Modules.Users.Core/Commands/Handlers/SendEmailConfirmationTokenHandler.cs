using Microsoft.Extensions.Logging;
using Stockfolio.Modules.Users.Core.Events;
using Stockfolio.Modules.Users.Core.Exceptions;
using Stockfolio.Modules.Users.Core.Managers;
using Stockfolio.Shared.Abstractions;
using Stockfolio.Shared.Abstractions.Commands;
using Stockfolio.Shared.Abstractions.Messaging;

namespace Stockfolio.Modules.Users.Core.Commands.Handlers;

internal sealed class GenerateEmailConfirmationTokenHandler : ICommandHandler<GenerateEmailConfirmationToken>
{
    private readonly UserManager _userManager;
    private readonly ILogger<ConfirmEmailHandler> _logger;
    private readonly IMessageBroker _messageBroker;

    public GenerateEmailConfirmationTokenHandler(UserManager userManager,
                                                 ILogger<ConfirmEmailHandler> logger,
                                                 IMessageBroker messageBroker)
    {
        _userManager = userManager;
        _logger = logger;
        _messageBroker = messageBroker;
    }

    public async Task HandleAsync(GenerateEmailConfirmationToken command, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(command.UserId.ToString())
                                        .NotNull(() => new UserNotFoundException(command.UserId));

        if (user.EmailConfirmed)
        {
            throw new EmailAlreadyConfirmedException();
        }

        var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        await _messageBroker.PublishAsync(new EmailConfirmationTokenGenerated(user.UserName, user.Email, emailConfirmationToken));
        _logger.LogInformation("User with ID: '{UserId}' has generated email confirmation token.", user.Id);
    }
}