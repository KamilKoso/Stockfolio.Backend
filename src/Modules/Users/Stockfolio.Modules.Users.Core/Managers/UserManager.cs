using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stockfolio.Modules.Users.Core.Entities;

namespace Stockfolio.Modules.Users.Core.Managers;

internal class UserManager : UserManager<User>
{
    public UserManager(IUserStore<User> store,
                       IOptions<IdentityOptions> optionsAccessor,
                       IPasswordHasher<User> passwordHasher,
                       IEnumerable<IUserValidator<User>> userValidators,
                       IEnumerable<IPasswordValidator<User>> passwordValidators,
                       ILookupNormalizer keyNormalizer,
                       IdentityErrorDescriber errors,
                       IServiceProvider services,
                       ILogger<UserManager> logger)
                       : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
    {
    }

    public Task<User> FindByIdAsync(Guid userId)
    {
        return FindByIdAsync(userId.ToString());
    }
}