using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecms.Infrastructure.Database.Configuration;

internal class AddressConfiguration : IEntityTypeConfiguration<AddressEntity>
{
    public void Configure(EntityTypeBuilder<AddressEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Country)
               .HasMaxLength(70)
               .IsRequired();

        builder.Property(p => p.City)
               .HasMaxLength(70)
               .IsRequired();

        builder.Property(p => p.Street)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(p => p.PostalCode)
               .HasMaxLength(11)
               .IsRequired();

        builder.Property(p => p.BuildingNumber)
               .HasMaxLength(8)
               .IsRequired();

        builder.Property(p => p.ApartmentNumber)
               .HasMaxLength(8)
               .IsRequired(false);
    }
}