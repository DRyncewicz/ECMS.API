using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecms.Infrastructure.Database.Configuration;

internal class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.CreateDateTimeUtc)
               .IsRequired()
               .HasPrecision(7);

        builder.Property(p => p.OrderNumber)
               .IsRequired();

        builder.Property(p => p.OrderStatus)
               .HasConversion<string>()
               .IsRequired();

        builder.Property(p => p.TableNumber)
               .IsRequired(false);

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

        builder.Property(p => p.UserId)
               .IsRequired()
               .HasMaxLength(450);

        builder.HasOne(p => p.Address)
               .WithMany(p => p.Orders)
               .HasForeignKey(p => p.AddressId)
               .IsRequired(false);
    }
}