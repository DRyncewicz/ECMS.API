using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecms.Infrastructure.Database.Configuration;

internal class ProductVariantAllergenConfiguration : IEntityTypeConfiguration<ProductVariantAllergenEntity>
{
    public void Configure(EntityTypeBuilder<ProductVariantAllergenEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Allergen)
               .WithMany(p => p.ProductVariantAllergens)
               .HasForeignKey(p => p.AllergenId);

        builder.HasOne(p => p.ProductVariant)
               .WithMany(p => p.ProductVariantAllergens)
               .HasForeignKey(p => p.ProductVariantId);
    }
}