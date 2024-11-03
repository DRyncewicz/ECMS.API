using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecms.Infrastructure.Database.Configuration;

internal class ProductMaterialConfiguration : IEntityTypeConfiguration<ProductMaterialEntity>
{
    public void Configure(EntityTypeBuilder<ProductMaterialEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.IsDeleted)
               .IsRequired();

        builder.Property(p => p.Quantity)
               .IsRequired();

        builder.HasOne(p => p.ProductVariant)
               .WithMany(p => p.ProductMaterials)
               .HasForeignKey(p => p.ProductVariantId);

        builder.HasOne(p => p.Material)
               .WithMany(p => p.ProductMaterials)
               .HasForeignKey(p => p.MaterialId);
    }
}