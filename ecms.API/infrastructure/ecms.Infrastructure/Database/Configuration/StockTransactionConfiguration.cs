using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecms.Infrastructure.Database.Configuration;

internal class StockTransactionConfiguration : IEntityTypeConfiguration<StockTransactionEntity>
{
    public void Configure(EntityTypeBuilder<StockTransactionEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.MaterialId)
               .IsRequired();

        builder.Property(p => p.Quantity)
               .IsRequired();

        builder.Property(p => p.TransactionType)
               .HasConversion<string>()
               .IsRequired();

        builder.Property(p => p.BatchNumber)
               .IsRequired(false)
               .HasMaxLength(70);

        builder.Property(p => p.ExpiryDate)
               .IsRequired(false)
               .HasPrecision(7);

        builder.Property(p => p.CreateDateTimeUtc)
               .IsRequired()
               .HasPrecision(7);

        builder.Property(p => p.UserId)
               .IsRequired()
               .HasMaxLength(450);

        builder.HasOne(p => p.StockLevel)
               .WithMany(p => p.StockTransactions)
               .HasForeignKey(p => p.StockLevelId);

        builder.HasOne(p => p.SupplierOrderMaterial)
               .WithOne(p => p.StockTransaction)
               .HasForeignKey<StockTransactionEntity>(p => p.SupplierOrderMaterialId)
               .IsRequired(false);

        builder.HasOne(p => p.Order)
               .WithOne(p => p.StockTransaction)
               .HasForeignKey<StockTransactionEntity>(p => p.OrderId)
               .IsRequired(false);
    }
}
