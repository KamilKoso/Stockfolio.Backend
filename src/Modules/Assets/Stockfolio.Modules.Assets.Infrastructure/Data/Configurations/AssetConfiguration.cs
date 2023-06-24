using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stockfolio.Modules.Assets.Core.Assets;

namespace Stockfolio.Modules.Assets.Infrastructure.Data.Configurations;

internal class AssetConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(AssetName.MaximumLength)
            .HasConversion(x => x.Value, x => new(x))
            .IsRequired();

        builder.Property(x => x.Currency)
           .HasMaxLength(3)
           .HasConversion(x => x.Code, x => new(x))
           .IsRequired();

        builder.Property(x => x.Owner)
          .HasMaxLength(36)
          .HasConversion(x => x.Value, x => new(x))
          .IsRequired();
    }
}