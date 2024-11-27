using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecms.Infrastructure.Database.Configuration;

internal class SupplierOrderConfiguration : IEntityTypeConfiguration<SupplierOrderEntity>
{
    public void Configure(EntityTypeBuilder<SupplierOrderEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.DeliveryDate)
            .IsRequired();

        builder.Property(p => p.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.HasOne(p => p.Supplier)
               .WithMany(p => p.SupplierOrders)
               .HasForeignKey(p => p.SupplierId);
    }
}