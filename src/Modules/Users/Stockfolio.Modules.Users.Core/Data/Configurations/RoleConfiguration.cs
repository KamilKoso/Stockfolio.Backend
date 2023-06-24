using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stockfolio.Modules.Users.Core.Entities;

namespace Stockfolio.Modules.Users.Core.Data.Configurations;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
    }
}