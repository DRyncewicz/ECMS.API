using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecms.Infrastructure.Database.Configuration;

internal class ProductVariantHistoryConfiguration : IEntityTypeConfiguration<ProductVariantHistoryEntity>
{
    public void Configure(EntityTypeBuilder<ProductVariantHistoryEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.ProductId)
               .IsRequired();

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
               .HasMaxLength(40)
               .IsRequired();

        builder.Property(p => p.IsDeleted)
               .IsRequired();

        builder.Property(p => p.CreatorUserId)
               .HasMaxLength(450)
               .IsRequired();

        builder.Property(p => p.CreateDateTimeUtc)
               .HasPrecision(7)
               .IsRequired();

        builder.HasOne(p => p.ProductVariant)
               .WithMany(p => p.ProductVariantHistories)
               .HasForeignKey(p => p.ProductVariantId);
    }
}