using Microsoft.Extensions.Logging;
using Stockfolio.Modules.Users.Core.Entities;
using Stockfolio.Modules.Users.Core.Events;
using Stockfolio.Modules.Users.Core.Exceptions;
using Stockfolio.Modules.Users.Core.Managers;
using Stockfolio.Modules.Users.Core.Services;
using Stockfolio.Shared.Abstractions;
using Stockfolio.Shared.Abstractions.Auth;
using Stockfolio.Shared.Abstractions.Commands;
using Stockfolio.Shared.Abstractions.Messaging;

namespace Stockfolio.Modules.Users.Core.Commands.Handlers;

internal sealed class SignInHandler : ICommandHandler<SignIn>
{
    private readonly UserManager _userManager;
    private readonly IJwtProvider _jwtProvider;
    private readonly IUserRequestStorage _userRequestStorage;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<SignInHandler> _logger;

    public SignInHandler(UserManager userManager,
                         IJwtProvider jwtProvider,
                         IUserRequestStorage userRequestStorage,
                         IMessageBroker messageBroker,
                         ILogger<SignInHandler> logger)
    {
        _userManager = userManager;
        _jwtProvider = jwtProvider;
        _userRequestStorage = userRequestStorage;
        _messageBroker = messageBroker;
        _logger = logger;
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

        var jwt = _jwtProvider.CreateToken(user.Id, user.UserRoles.Select(x => x.Role.Name));
        jwt.Email = user.Email;
        await _messageBroker.PublishAsync(new SignedIn(user.Id), cancellationToken);
        _logger.LogInformation($"User with ID: '{user.Id}' has signed in.");
        _userRequestStorage.SetToken(command.Id, jwt);
    }
}