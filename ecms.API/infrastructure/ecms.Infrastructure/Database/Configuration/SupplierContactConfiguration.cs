using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecms.Infrastructure.Database.Configuration;

internal class SupplierContactConfiguration : IEntityTypeConfiguration<SupplierContactEntity>
{
    public void Configure(EntityTypeBuilder<SupplierContactEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.IsActive)
               .IsRequired();

        builder.Property(p => p.IsCommon)
               .IsRequired();

        builder.Property(p => p.RepresentativeName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.PhoneNumber)
               .IsRequired()
               .HasMaxLength(22);

        builder.Property(p => p.Email)
               .IsRequired()
               .HasMaxLength(254);

        builder.Property(p => p.Description)
               .IsRequired()
               .HasMaxLength(500);

        builder.HasOne(p => p.Supplier)
               .WithMany(p => p.SupplierContacts)
               .HasForeignKey(p => p.SupplierId);
    }
}
