using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecms.Infrastructure.Database.Configuration;

internal class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
               .HasMaxLength(40)
               .IsRequired();

        builder.Property(p => p.Description)
               .HasMaxLength(1000)
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

        builder.Property(p => p.UserId)
               .HasMaxLength(450)
               .IsRequired();

        builder.Property(p => p.IsDeleted)
               .IsRequired();

        builder.HasOne(p => p.Category)
               .WithMany(p => p.Products)
               .HasForeignKey(p => p.CategoryId);
    }
}
