using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Stockfolio.Modules.Users.Core.Entities;
using Stockfolio.Modules.Users.Core.Events;
using Stockfolio.Modules.Users.Core.Exceptions;
using Stockfolio.Modules.Users.Core.Managers;
using Stockfolio.Modules.Users.Core.Options;
using Stockfolio.Shared.Abstractions.Commands;
using Stockfolio.Shared.Abstractions.Kernel.Exceptions;
using Stockfolio.Shared.Abstractions.Messaging;
using Stockfolio.Shared.Abstractions.Time;

namespace Stockfolio.Modules.Users.Core.Commands.Handlers;

internal sealed class SignUpHandler : ICommandHandler<SignUp>
{
    private readonly UserManager _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IClock _clock;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<SignUpHandler> _logger;
    private readonly RegistrationOptions _registrationOptions;
    private readonly Options.IdentityOptions _identityOptions;

    public SignUpHandler(UserManager userManager,
                         RoleManager<Role> roleManager,
                         IClock clock,
                         IMessageBroker messageBroker,
                         ILogger<SignUpHandler> logger,
                         RegistrationOptions registrationOptions,
                         Options.IdentityOptions identityOptions)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _clock = clock;
        _messageBroker = messageBroker;
        _logger = logger;
        _registrationOptions = registrationOptions;
        _identityOptions = identityOptions;
    }

    public async Task HandleAsync(SignUp command, CancellationToken cancellationToken = default)
    {
        if (!_registrationOptions.Enabled)
        {
            throw new SignUpDisabledException();
        }

        var provider = command.Email.Split("@").Last();
        if (_registrationOptions.InvalidEmailProviders?.Any(x => provider.Contains(x)) is true)
        {
            throw new InvalidEmailException(command.Email);
        }

        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user is not null)
        {
            throw new EmailInUseException();
        }

        var role = await _roleManager.FindByNameAsync(Role.Default);
        var now = _clock.CurrentDate();
        user = new User
        {
            Id = command.UserId,
            Email = command.Email,
            Roles = new[] { role },
            CreatedAt = now,
            State = UserState.Active,
        };

        var result = await _userManager.CreateAsync(user, command.Password);
        var error = result.Errors.FirstOrDefault();

        if (error is not null)
        {
            throw new SignUpException(error.Description, error.Code);
        }

        await _messageBroker.PublishAsync(new SignedUp(user.Id, command.Email, role.Name), cancellationToken);
        _logger.LogInformation($"User with ID: '{user.Id}' has signed up.");
    }
}