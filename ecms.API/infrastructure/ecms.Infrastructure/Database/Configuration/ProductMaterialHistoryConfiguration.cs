using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecms.Infrastructure.Database.Configuration;

internal class ProductMaterialHistoryConfiguration : IEntityTypeConfiguration<ProductMaterialHistoryEntity>
{
    public void Configure(EntityTypeBuilder<ProductMaterialHistoryEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.ProductVariantId)
               .IsRequired();

        builder.Property(p => p.MaterialId)
               .IsRequired();

        builder.Property(p => p.Quantity)
               .IsRequired();

        builder.Property(p => p.IsDeleted)
               .IsRequired();

        builder.Property(p => p.CreateDateTimeUtc)
               .IsRequired()
               .HasPrecision(7);

        builder.Property(p => p.CreatorUserId)
               .IsRequired()
               .HasMaxLength(450);

        builder.HasOne(p => p.ProductMaterial)
               .WithMany(p => p.ProductMaterialHistories)
               .HasForeignKey(p => p.ProductMaterialId);
    }
}