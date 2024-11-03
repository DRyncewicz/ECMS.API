using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecms.Infrastructure.Database.Configuration;

internal class AllergenConfiguration : IEntityTypeConfiguration<AllergenEntity>
{
    public void Configure(EntityTypeBuilder<AllergenEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(40);
    }
}