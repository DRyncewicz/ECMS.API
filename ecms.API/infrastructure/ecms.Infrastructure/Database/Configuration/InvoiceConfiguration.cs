using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecms.Infrastructure.Database.Configuration;

internal class InvoiceConfiguration : IEntityTypeConfiguration<InvoiceEntity>
{
    public void Configure(EntityTypeBuilder<InvoiceEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.InvoiceNumber)
               .IsRequired()
               .HasMaxLength(30);

        builder.OwnsOne(x => x.TotalPrice, p =>
        {
            p.Property(x => x.Amount)
                .IsRequired()
                .HasColumnName("PriceAmount");

            p.Property(x => x.Currency)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnName("PriceCurrency");
        });

        builder.Property(p => p.FileGuid)
               .IsRequired();

        builder.Property(p => p.CreateDateTimeUtc)
               .IsRequired()
               .HasPrecision(7);
    }
}