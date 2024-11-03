using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecms.Infrastructure.Database.Configuration;

internal class StockLevelConfiguration : IEntityTypeConfiguration<StockLevelEntity>
{
    public void Configure(EntityTypeBuilder<StockLevelEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Quantity)
               .IsRequired();

        builder.Property(p => p.IsDeleted)
               .IsRequired();

        builder.Property(p => p.BatchNumber)
               .IsRequired(false)
               .HasMaxLength(70);

        builder.Property(p => p.CreateDateTimeUtc)
               .IsRequired()
               .HasPrecision(7);

        builder.Property(p => p.LastUpdated)
               .IsRequired(false)
               .HasPrecision(7);

        builder.HasOne(p => p.Stock)
               .WithMany(p => p.StockLevels)
               .HasForeignKey(p => p.StockId);

        builder.HasOne(p => p.Material)
               .WithOne(p => p.StockLevel)
               .HasForeignKey<StockLevelEntity>(p => p.MaterialId);
    }
}