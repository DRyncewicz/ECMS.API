using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecms.Infrastructure.Database.Configuration;

internal class MaterialHistoryConfiguration : IEntityTypeConfiguration<MaterialHistoryEntity>
{
    public void Configure(EntityTypeBuilder<MaterialHistoryEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(p => p.IsActive)
               .IsRequired();

        builder.Property(p => p.IsDeleted)
               .IsRequired();

        builder.Property(p => p.UnitOfMeasure)
               .HasConversion<string>()
               .IsRequired();

        builder.Property(p => p.Description)
               .HasMaxLength(500)
               .IsRequired();

        builder.Property(p => p.MinStockLevel)
               .IsRequired();

        builder.Property(p => p.MaxStockLevel)
               .IsRequired();

        builder.Property(p => p.ReorderLevel)
               .IsRequired();

        builder.Property(p => p.FileGuid)
               .IsRequired(false);

        builder.HasOne(p => p.Material)
               .WithMany(p => p.MaterialHistories)
               .HasForeignKey(p => p.MaterialId);
    }
}
