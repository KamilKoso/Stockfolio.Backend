using Microsoft.AspNetCore.Identity;

namespace Stockfolio.Modules.Users.Core.Validators;

internal class UserErrorDescriber : IdentityErrorDescriber
{
    public IdentityError InvalidEmailProvider(string provider)
    { return new IdentityError { Code = nameof(InvalidEmailProvider), Description = $"Invalid email provider. '{provider}' is not accepted email provider." }; }
}