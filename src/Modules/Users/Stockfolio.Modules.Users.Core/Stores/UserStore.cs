using Stockfolio.Modules.Users.Core.DAL;
using Stockfolio.Modules.Users.Core.Entities;
using Identity = Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Stockfolio.Modules.Users.Core.Stores;

internal class UserStore : Identity.UserStore<User, Role, UsersDbContext, Guid, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>
{
    public UserStore(UsersDbContext context)
        : base(context)
    {
    }
}