using Microsoft.EntityFrameworkCore;
using Stockfolio.Modules.Users.Core.Entities;

namespace Stockfolio.Modules.Users.Core.Stores;

internal static class Extensions
{
    public static IQueryable<User> EagerLoad(this DbSet<User> dbSet)
    => dbSet
        .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
        .Include(x => x.UserClaims)
        .Include(x => x.UserLogins);
}