using Microsoft.Extensions.Logging;
using Stockfolio.Modules.Users.Core.Events;
using Stockfolio.Modules.Users.Core.Exceptions;
using Stockfolio.Modules.Users.Core.Managers;
using Stockfolio.Shared.Abstractions;
using Stockfolio.Shared.Abstractions.Commands;
using Stockfolio.Shared.Abstractions.Messaging;

namespace Stockfolio.Modules.Users.Core.Queries.Handlers;

internal sealed class ConfirmEmailHandler : ICommandHandler<ConfirmEmail>
{
    private readonly UserManager _userManager;
    private readonly ILogger<ConfirmEmailHandler> _logger;
    private readonly IMessageBroker _messageBroker;

    public ConfirmEmailHandler(UserManager userManager,
                                                 ILogger<ConfirmEmailHandler> logger,
                                                 IMessageBroker messageBroker)
    {
        _userManager = userManager;
        _logger = logger;
        _messageBroker = messageBroker;
    }

    public async Task HandleAsync(ConfirmEmail command, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(command.UserId.ToString())
                                        .NotNull(() => new UserNotFoundException(command.UserId));

        if (user.EmailConfirmed)
        {
            throw new EmailAlreadyConfirmedException();
        }

        var result = await _userManager.ConfirmEmailAsync(user, command.EmailConfirmationToken);
        if (result.Errors.Any())
        {
            throw new ConfirmEmailException(result.Errors.First().Description);
        }

        await _messageBroker.PublishAsync(new EmailConfirmed(user.Id), cancellationToken);
        _logger.LogInformation($"User with ID: '{user.Id}' confirmed his email.");
    }
}