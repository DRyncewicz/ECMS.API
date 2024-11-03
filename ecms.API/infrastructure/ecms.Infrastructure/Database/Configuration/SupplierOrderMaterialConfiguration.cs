using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecms.Infrastructure.Database.Configuration;

internal class SupplierOrderMaterialConfiguration : IEntityTypeConfiguration<SupplierOrderMaterialEntity>
{
    public void Configure(EntityTypeBuilder<SupplierOrderMaterialEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.IsDelivered)
               .IsRequired();

        builder.Property(p => p.Quantity)
               .IsRequired();

        builder.OwnsOne(x => x.PricePerUnit, p =>
        {
            p.Property(x => x.Amount)
                .IsRequired()
                .HasColumnName("PriceAmount");

            p.Property(x => x.Currency)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnName("PriceCurrency");
        });

        builder.HasOne(p => p.SupplierOrder)
               .WithMany(p => p.SupplierOrderMaterials)
               .HasForeignKey(p => p.SupplierOrderId);

        builder.HasOne(p => p.Material)
               .WithMany(p => p.SuppliersOrderMaterials)
               .HasForeignKey(p => p.MaterialId);
    }
}