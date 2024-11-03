using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecms.Infrastructure.Database.Configuration;

internal class SupplierConfiguration : IEntityTypeConfiguration<SupplierEntity>
{
    public void Configure(EntityTypeBuilder<SupplierEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.IsActive)
               .IsRequired();

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(70);

        builder.HasOne(p => p.Address)
               .WithMany(p => p.Suppliers)
               .HasForeignKey(p => p.AddressId);
    }
}