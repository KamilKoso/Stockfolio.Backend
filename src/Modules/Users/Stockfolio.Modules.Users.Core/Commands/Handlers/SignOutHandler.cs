using Microsoft.Extensions.Logging;
using Stockfolio.Modules.Users.Core.Events;
using Stockfolio.Shared.Abstractions.Commands;
using Stockfolio.Shared.Abstractions.Messaging;

namespace Stockfolio.Modules.Users.Core.Commands.Handlers;

internal sealed class SignOutHandler : ICommandHandler<SignOut>
{
    private readonly ILogger<SignOutHandler> _logger;
    private readonly IMessageBroker _messageBroker;

    public SignOutHandler(ILogger<SignOutHandler> logger,
                          IMessageBroker messageBroker)
    {
        _logger = logger;
        _messageBroker = messageBroker;
    }

    public async Task HandleAsync(SignOut command, CancellationToken cancellationToken = default)
    {
        await _messageBroker.PublishAsync(new SignedOut(command.UserId), cancellationToken);
        _logger.LogInformation($"User with ID: '{command.UserId}' has signed out.");
    }
}