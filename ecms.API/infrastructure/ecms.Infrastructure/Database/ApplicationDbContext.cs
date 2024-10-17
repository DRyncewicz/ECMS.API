using ecms.Application.Abstractions.Data;
using ecms.Domain.Entities;
using ecms.Infrastructure.Database.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace ecms.Infrastructure.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<ProductEntity> Products { get; set; }

    public DbSet<CategoryEntity> Categories { get; set; }

    public DbSet<ProductHistoryEntity> ProductHistories { get; set; }

    public DbSet<ProductVariantEntity> ProductVariants { get; set; }

    public DbSet<ProductVariantHistoryEntity> ProductVariantHistories { get; set; }

    public DbSet<AddressEntity> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema.Ecms);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.DataSeed();
    }

    public async Task<IDbTransaction> BeginTransactionAsync(CancellationToken ct)
    {
        return (await Database.BeginTransactionAsync(ct)).GetDbTransaction();
    }
}