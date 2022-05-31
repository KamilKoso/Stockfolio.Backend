using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Stockfolio.Modules.Users.Core.Entities;
using Stockfolio.Modules.Users.Core.Exceptions;
using Stockfolio.Modules.Users.Core.Options;
using Stockfolio.Modules.Users.Core.Repositories;
using Stockfolio.Shared.Abstractions;
using Stockfolio.Shared.Abstractions.Commands;

namespace Stockfolio.Modules.Users.Core.Commands.Handlers;

internal sealed class ChangePasswordHandler : ICommandHandler<ChangePassword>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly PasswordStrengthPolicyOptions _passwordStrengthPolicyOptions;
    private readonly ILogger<ChangePasswordHandler> _logger;

    public ChangePasswordHandler(IUserRepository userRepository,
                                 IPasswordHasher<User> passwordHasher,
                                 PasswordStrengthPolicyOptions passwordStrengthPolicyOptions,
                                 ILogger<ChangePasswordHandler> logger)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _passwordStrengthPolicyOptions = passwordStrengthPolicyOptions;
        _logger = logger;
    }

    public async Task HandleAsync(ChangePassword command, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(command.UserId)
                                        .NotNull(() => new UserNotFoundException(command.UserId));

        _passwordStrengthPolicyOptions.Validate(command.NewPassword);
        if (_passwordHasher.VerifyHashedPassword(user, user.Password, command.CurrentPassword) ==
            PasswordVerificationResult.Failed)
        {
            throw new InvalidPasswordException("Current password is invalid");
        }

        user.Password = _passwordHasher.HashPassword(user, command.NewPassword);
        await _userRepository.UpdateAsync(user);
        _logger.LogInformation($"User with ID: '{user.Id}' has changed the password.");
    }
}