using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecms.Infrastructure.Database.Configuration;

internal class MessageConfiguration : IEntityTypeConfiguration<MessageEntity>
{
    public void Configure(EntityTypeBuilder<MessageEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Address)
               .IsRequired()
               .HasMaxLength(70);

        builder.Property(p => p.Content)
               .IsRequired();

        builder.Property(p => p.Subject)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.SentDateTimeUtc)
               .IsRequired()
               .HasPrecision(7);

        builder.Property(p => p.MessageStatus)
               .HasConversion<string>()
               .IsRequired();

        builder.Property(p => p.ErrorCode)
               .IsRequired(false);

        builder.Property(p => p.ErrorMessage)
               .IsRequired(false)
               .HasMaxLength(150);

        builder.Property(p => p.ErrorAttempts)
               .IsRequired();

        builder.Property(p => p.CreateDateTimeUtc)
               .IsRequired()
               .HasPrecision(7);
    }
}