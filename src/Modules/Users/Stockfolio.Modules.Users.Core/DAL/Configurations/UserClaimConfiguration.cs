using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stockfolio.Modules.Users.Core.Entities;

namespace Stockfolio.Modules.Users.Core.DAL.Configurations;

internal class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.UserClaims)
            .HasForeignKey(x => x.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}