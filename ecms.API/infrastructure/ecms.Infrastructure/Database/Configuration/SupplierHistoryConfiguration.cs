using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecms.Infrastructure.Database.Configuration;

internal class SupplierHistoryConfiguration : IEntityTypeConfiguration<SupplierHistoryEntity>
{
    public void Configure(EntityTypeBuilder<SupplierHistoryEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.IsActive)
               .IsRequired();

        builder.Property(p => p.IsDeleted)
               .IsRequired();

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(70);

        builder.Property(p => p.AddressId)
               .IsRequired();

        builder.Property(p => p.CreatorUserId)
               .IsRequired()
               .HasMaxLength(450);

        builder.Property(p => p.CreateDateTimeUtc)
               .IsRequired()
               .HasPrecision(7);

        builder.HasOne(p => p.Supplier)
               .WithMany(p => p.SupplierHistories)
               .HasForeignKey(p => p.SupplierId);
    }
}