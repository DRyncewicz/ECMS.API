using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecms.Infrastructure.Database.Configuration;

internal class OrderProductVariantConfiguration : IEntityTypeConfiguration<OrderProductVariantEntity>
{
    public void Configure(EntityTypeBuilder<OrderProductVariantEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Quantity)
               .IsRequired();

        builder.HasOne(p => p.Order)
               .WithMany(p => p.OrderProductVariants)
               .HasForeignKey(p => p.OrderId);

        builder.HasOne(p => p.ProductVariant)
               .WithMany(p => p.OrderProductVariants)
               .HasForeignKey(p => p.ProductVariantId);
    }
}
