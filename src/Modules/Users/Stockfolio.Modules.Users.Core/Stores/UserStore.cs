using Microsoft.EntityFrameworkCore;
using Stockfolio.Modules.Users.Core.DAL;
using Stockfolio.Modules.Users.Core.Entities;
using Identity = Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Stockfolio.Modules.Users.Core.Stores;

internal class UserStore : Identity.UserStore<User, Role, UsersDbContext, Guid, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>
{
    private readonly DbSet<User> _dbSet;

    public UserStore(UsersDbContext context)
        : base(context)
    {
        _dbSet = context.Set<User>();
    }

    public override async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
     => await _dbSet
            .EagerLoad()
            .FirstOrDefaultAsync(x => x.Id == new Guid(userId), cancellationToken);

    public override async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default)
    => await _dbSet
            .EagerLoad()
            .FirstOrDefaultAsync(x => x.NormalizedEmail == normalizedEmail, cancellationToken);

    public override async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default)
    => await _dbSet
            .EagerLoad()
            .FirstOrDefaultAsync(x => x.NormalizedUserName == normalizedUserName, cancellationToken);
}