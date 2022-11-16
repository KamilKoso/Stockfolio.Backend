using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Stockfolio.Modules.Users.Core.Entities;
using Stockfolio.Modules.Users.Core.Events;
using Stockfolio.Modules.Users.Core.Exceptions;
using Stockfolio.Modules.Users.Core.Managers;
using Stockfolio.Shared.Abstractions;
using Stockfolio.Shared.Abstractions.Commands;
using Stockfolio.Shared.Abstractions.Messaging;

namespace Stockfolio.Modules.Users.Core.Commands.Handlers;

internal sealed class SignInHandler : ICommandHandler<SignIn>
{
    private readonly UserManager _userManager;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<SignInHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SignInHandler(UserManager userManager,
                         IMessageBroker messageBroker,
                         ILogger<SignInHandler> logger,
                         IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _messageBroker = messageBroker;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task HandleAsync(SignIn command, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(command.Email)
                                        .NotNull(() => new UserNotFoundException(command.Email));

        if (user.State != UserState.Active)
        {
            throw new UserNotActiveException(user.Id);
        }

        if (!await _userManager.CheckPasswordAsync(user, command.Password))
        {
            throw new InvalidCredentialsException();
        }

        await _httpContextAccessor.HttpContext.SignInAsStockfolioUser(user);
        await _messageBroker.PublishAsync(new SignedIn(user.Id), cancellationToken);
        _logger.LogInformation($"User with ID: '{user.Id}' has signed in.");
    }
}