using Stockfolio.Modules.Users.Core.Exceptions;
using StockFolio.Shared.Infrastructure;

namespace Stockfolio.Modules.Users.Core.Options;

internal class PasswordStrengthPolicyOptions
{
    public int? MaxLength { get; init; }
    public int? MinLength { get; init; }
    public bool RequireSpecialCharacter { get; init; }
    public bool RequireUppercaseCharacter { get; init; }
    public bool RequireNumber { get; init; }

    public void Validate(string password)
    {
        if (string.IsNullOrEmpty(password))
            throw new InvalidPasswordException("Password cannot be empty.");
        else if (password.Length > MaxLength || password.Length < MinLength)
            throw new InvalidPasswordException($"Password length should be {MinLength} - {MaxLength} long.");
        else if (RequireNumber && !password.AnyNumbers())
            throw new InvalidPasswordException("Password requires at least one number.");
        else if (RequireUppercaseCharacter && !password.AnyUppercase())
            throw new InvalidPasswordException("Password requires at least one uppercase character.");
        else if (RequireSpecialCharacter && !password.AnySpecialCharacters())
            throw new InvalidPasswordException("Password requires at least one special character.");
    }
}