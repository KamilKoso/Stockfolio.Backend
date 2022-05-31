using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Stockfolio.Modules.Users.Core.Entities;
using Stockfolio.Modules.Users.Core.Events;
using Stockfolio.Modules.Users.Core.Exceptions;
using Stockfolio.Modules.Users.Core.Options;
using Stockfolio.Modules.Users.Core.Repositories;
using Stockfolio.Shared.Abstractions.Commands;
using Stockfolio.Shared.Abstractions.Kernel.Exceptions;
using Stockfolio.Shared.Abstractions.Messaging;
using Stockfolio.Shared.Abstractions.Time;

namespace Stockfolio.Modules.Users.Core.Commands.Handlers;

internal sealed class SignUpHandler : ICommandHandler<SignUp>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IClock _clock;
    private readonly IMessageBroker _messageBroker;
    private readonly RegistrationOptions _registrationOptions;
    private readonly PasswordStrengthPolicyOptions _passwordStrengthPolicyOptions;
    private readonly ILogger<SignUpHandler> _logger;

    public SignUpHandler(IUserRepository userRepository,
                         IRoleRepository roleRepository,
                         IPasswordHasher<User> passwordHasher,
                         IClock clock,
                         IMessageBroker messageBroker,
                         RegistrationOptions registrationOptions,
                         PasswordStrengthPolicyOptions passwordStrengthPolicyOptions,
                         ILogger<SignUpHandler> logger)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _passwordHasher = passwordHasher;
        _clock = clock;
        _messageBroker = messageBroker;
        _registrationOptions = registrationOptions;
        _passwordStrengthPolicyOptions = passwordStrengthPolicyOptions;
        _logger = logger;
    }

    public async Task HandleAsync(SignUp command, CancellationToken cancellationToken = default)
    {
        ValidateCommand(command);
        var user = await _userRepository.GetAsync(command.Email);
        if (user is not null)
        {
            throw new EmailInUseException();
        }

        var role = await _roleRepository.GetAsync(Role.Default);
        var now = _clock.CurrentDate();
        var password = _passwordHasher.HashPassword(default, command.Password);
        user = new User
        {
            Id = command.UserId,
            Email = command.Email,
            Password = password,
            Role = role,
            CreatedAt = now,
            State = UserState.Active,
        };
        await _userRepository.AddAsync(user);
        await _messageBroker.PublishAsync(new SignedUp(user.Id, command.Email, role.Name), cancellationToken);
        _logger.LogInformation($"User with ID: '{user.Id}' has signed up.");
    }

    private void ValidateCommand(SignUp command)
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

        _passwordStrengthPolicyOptions.Validate(command.Password);
    }
}