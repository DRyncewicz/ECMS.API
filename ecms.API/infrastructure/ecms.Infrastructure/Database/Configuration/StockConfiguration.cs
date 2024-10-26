using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecms.Infrastructure.Database.Configuration;

internal class StockConfiguration : IEntityTypeConfiguration<StockEntity>
{
    public void Configure(EntityTypeBuilder<StockEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(p => p.Description)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(p => p.IsDeleted)
               .IsRequired();

        builder.HasOne(p => p.Address)
               .WithMany(p => p.Stocks)
               .HasForeignKey(p => p.AddressId);
    }
}
