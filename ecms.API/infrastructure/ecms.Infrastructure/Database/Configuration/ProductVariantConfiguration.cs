using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecms.Infrastructure.Database.Configuration;

internal class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariantEntity>
{
    public void Configure(EntityTypeBuilder<ProductVariantEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.OwnsOne(x => x.Price, p =>
        {
            p.Property(x => x.Amount)
                .IsRequired()
                .HasColumnName("PriceAmount");

            p.Property(x => x.Currency)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnName("PriceCurrency");
        });

        builder.Property(p => p.Name)
               .HasMaxLength(60)
               .IsRequired();

        builder.Property(p => p.IsDeleted)
               .IsRequired();

        builder.HasOne(p => p.Product)
               .WithMany(p => p.ProductVariants)
               .HasForeignKey(p => p.ProductId);
    }
}