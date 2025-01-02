using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecms.Infrastructure.Database.Configuration;

internal class SupplierOrderConfiguration : IEntityTypeConfiguration<SupplierOrderEntity>
{
    public void Configure(EntityTypeBuilder<SupplierOrderEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.MessageId);

        builder.Property(p => p.DeliveryDate);

        builder.Property(p => p.Status)
               .IsRequired()
               .HasConversion<string>();

        builder.HasOne(p => p.Supplier)
               .WithMany(p => p.SupplierOrders)
               .HasForeignKey(p => p.SupplierId);

        builder.HasOne(p => p.Message)
               .WithOne(p => p.SupplierOrder)
               .HasForeignKey<SupplierOrderEntity>(p => p.MessageId);
    }
}