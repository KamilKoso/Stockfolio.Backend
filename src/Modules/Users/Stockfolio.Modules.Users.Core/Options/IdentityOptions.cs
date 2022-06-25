using Microsoft.AspNetCore.Identity;

namespace Stockfolio.Modules.Users.Core.Options;

internal class IdentityOptions
{
    public bool RegistrationEnabled { get; init; }
    public IEnumerable<string> InvalidEmailProviders { get; init; }
    public bool RequireUniqueEmail { get; init; }
    public PasswordOptions PasswordOptions { get; init; }
    public SignInOptions SignInOptions { get; init; }
}