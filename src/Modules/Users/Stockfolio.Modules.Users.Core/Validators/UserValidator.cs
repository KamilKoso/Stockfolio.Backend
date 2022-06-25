using Microsoft.AspNetCore.Identity;
using Stockfolio.Modules.Users.Core.Entities;

namespace Stockfolio.Modules.Users.Core.Validators;

internal class UserValidator : UserValidator<User>
{
    private readonly Options.IdentityOptions _identityOptions;

    public UserValidator(Options.IdentityOptions identityOptions, IdentityErrorDescriber errors) : base(errors)

    {
        _identityOptions = identityOptions;
    }

    public override async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
    {
        var result = await base.ValidateAsync(manager, user);
        var errors = new List<IdentityError>(result.Errors);

        var provider = user.Email.Split("@").Last();
        if (_identityOptions.InvalidEmailProviders?.Any(x => provider.Contains(x)) is true)
        {
            errors.Add(((UserErrorDescriber)Describer).InvalidEmailProvider(provider));
        }

        return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
    }
}