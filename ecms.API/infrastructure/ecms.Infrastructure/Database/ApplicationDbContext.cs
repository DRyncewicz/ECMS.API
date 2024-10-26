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

    public DbSet<AllergenEntity> Allergens { get; set; }

    public DbSet<InvoiceEntity> Invoices { get; set; }

    public DbSet<MaterialEntity> Materials { get; set; }

    public DbSet<MaterialHistoryEntity> MaterialHistories { get; set; }

    public DbSet<MessageEntity> Messages { get; set; }

    public DbSet<OrderEntity> Orders { get; set; }

    public DbSet<OrderProductVariantEntity> OrderProductVariants { get; set; }

    public DbSet<ProductMaterialEntity> ProductMaterials { get; set; }

    public DbSet<ProductMaterialHistoryEntity> ProductMaterialHistories { get; set; }

    public DbSet<ProductVariantAllergenEntity> ProductVariantAllergens { get; set; }

    public DbSet<StockEntity> Stocks { get; set; }

    public DbSet<StockLevelEntity> StockLevels { get; set; }

    public DbSet<StockTransactionEntity> StockTransactions { get; set; }

    public DbSet<SupplierContactEntity> SupplierContacts { get; set; }

    public DbSet<SupplierEntity> Suppliers { get; set; }

    public DbSet<SupplierHistoryEntity> SupplierHistories { get; set; }

    public DbSet<SupplierOrderEntity> SupplierOrders { get; set; }

    public DbSet<SupplierOrderMaterialEntity> SupplierOrderMaterials { get; set; }

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