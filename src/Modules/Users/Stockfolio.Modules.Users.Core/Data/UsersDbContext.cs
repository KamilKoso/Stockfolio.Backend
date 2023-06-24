using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stockfolio.Modules.Users.Core.Entities;
using Stockfolio.Shared.Infrastructure.Messaging.Outbox;

namespace Stockfolio.Modules.Users.Core.Data;

internal class UsersDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
    public DbSet<InboxMessage> Inbox { get; set; }
    public DbSet<OutboxMessage> Outbox { get; set; }

    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Keep this line first so ASP .NET Identity registers its default, which later could be override
        modelBuilder.HasDefaultSchema("users");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}

// dotnet ef migrations add Init --context UsersDbContext --startup-project ../../../Bootstrapper/Stockfolio.Bootstrapper -o ./DAL/Migrations