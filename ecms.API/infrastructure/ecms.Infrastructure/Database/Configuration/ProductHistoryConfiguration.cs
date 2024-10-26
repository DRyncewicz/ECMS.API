using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecms.Infrastructure.Database.Configuration;

internal class ProductHistoryConfiguration : IEntityTypeConfiguration<ProductHistoryEntity>
{
    public void Configure(EntityTypeBuilder<ProductHistoryEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
               .HasMaxLength(40)
               .IsRequired();

        builder.Property(p => p.Description)
               .HasMaxLength(1000)
               .IsRequired();

        builder.Property(p => p.CategoryId)
               .IsRequired();

        builder.Property(p => p.Vat)
               .IsRequired();

        builder.Property(p => p.Unit)
               .IsRequired()
               .HasConversion<string>();

        builder.Property(p => p.AlcoholContent)
               .IsRequired()
               .HasConversion<string>();

        builder.Property(p => p.GtuCode)
               .IsRequired(false)
               .HasConversion<string>();

        builder.Property(p => p.FileGuid)
               .IsRequired(false);

        builder.Property(p => p.IsDeleted)
               .IsRequired();

        builder.Property(p => p.CreateDateTimeUtc)
               .HasPrecision(7)
               .IsRequired();

        builder.Property(p => p.CreatorUserId)
               .HasMaxLength(450)
               .IsRequired();

        builder.HasOne(p => p.Product)
               .WithMany(p => p.ProductHistories)
               .HasForeignKey(p => p.ProductId);
    }
}