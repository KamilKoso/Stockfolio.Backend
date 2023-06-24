using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stockfolio.Modules.Users.Core.Entities;

namespace Stockfolio.Modules.Users.Core.Data.Configurations;

internal class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.UserLogins)
            .HasForeignKey(x => x.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}