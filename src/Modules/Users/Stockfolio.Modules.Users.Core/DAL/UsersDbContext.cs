using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stockfolio.Modules.Users.Core.Entities;
using Stockfolio.Shared.Infrastructure.Messaging.Outbox;

namespace Stockfolio.Modules.Users.Core.DAL;

internal class UsersDbContext : IdentityDbContext<User, Role, Guid>
{
    public DbSet<InboxMessage> Inbox { get; set; }
    public DbSet<OutboxMessage> Outbox { get; set; }

    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("users");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

// dotnet ef migrations add Init --context UsersDbContext --startup-project ../../../Bootstrapper/Stockfolio.Bootstrapper -o ./DAL/Migrations