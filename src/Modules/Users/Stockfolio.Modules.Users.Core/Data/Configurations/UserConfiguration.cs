using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stockfolio.Modules.Users.Core.Entities;

namespace Stockfolio.Modules.Users.Core.Data.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.Email).IsRequired().HasMaxLength(500);
    }
}