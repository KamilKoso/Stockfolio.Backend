using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Stockfolio.Modules.Users.Core.DAL;
using Stockfolio.Modules.Users.Core.Entities;

namespace Stockfolio.Modules.Users.Core.Stores;

internal class UserStore : UserStore<User, Role, UsersDbContext, Guid>
{
    public UserStore(UsersDbContext context)
        : base(context)
    {
    }
}