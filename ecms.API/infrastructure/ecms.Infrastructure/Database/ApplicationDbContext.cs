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

    public DbSet<AllergenEntity> Allergen { get; set; }

    public DbSet<InvoiceEntity> Invoice { get; set; }

    public DbSet<MaterialEntity> Material { get; set; }

    public DbSet<MaterialHistoryEntity> MaterialHistory { get; set; }

    public DbSet<MessageEntity> Message { get; set; }

    public DbSet<OrderEntity> Order { get; set; }

    public DbSet<OrderProductVariantEntity> OrderProductVariant { get; set; }

    public DbSet<ProductMaterialEntity> ProductMaterial { get; set; }

    public DbSet<ProductMaterialHistoryEntity> ProductMaterialHistory { get; set; }

    public DbSet<ProductVariantAllergenEntity> ProductVariantAllergen { get; set; }

    public DbSet<StockEntity> Stock { get; set; }

    public DbSet<StockLevelEntity> StockLevel { get; set; }

    public DbSet<StockTransactionEntity> StockTransaction { get; set; }

    public DbSet<SupplierContactEntity> SupplierContact { get; set; }

    public DbSet<SupplierEntity> Supplier { get; set; }

    public DbSet<SupplierHistoryEntity> SupplierHistory { get; set; }

    public DbSet<SupplierOrderEntity> SupplierOrder { get; set; }

    public DbSet<SupplierOrderMaterialEntity> SupplierOrderMaterial { get; set; }

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