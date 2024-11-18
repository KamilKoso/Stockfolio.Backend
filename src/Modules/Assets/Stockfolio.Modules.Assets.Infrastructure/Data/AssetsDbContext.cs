using Microsoft.EntityFrameworkCore;
using Stockfolio.Modules.Assets.Core.Assets;

namespace Stockfolio.Modules.Assets.Infrastructure.Data;

internal class AssetsDbContext : DbContext
{
    public const string VersionColumn = "Version";
    public DbSet<Asset> Assets { get; set; }

    public AssetsDbContext(DbContextOptions<AssetsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("assets");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateVersions();

        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        UpdateVersions();
        return base.SaveChanges();
    }

    private void UpdateVersions()
    {
        foreach (var change in ChangeTracker.Entries())
        {
            try
            {
                var versionProperty = change.Member(VersionColumn);
                versionProperty.CurrentValue = Guid.NewGuid();
            }
            catch
            {
            }
        }
    }
}

// dotnet ef migrations add Init --context UsersDbContext --startup-project ../../../Bootstrapper/Stockfolio.Bootstrapper -o ./DAL/Migrations