using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ecms.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<IDbTransaction> BeginTransactionAsync(CancellationToken ct = default);

    DbSet<ProductEntity> Products { get; set; }

    DbSet<CategoryEntity> Categories { get; set; }

    DbSet<ProductHistoryEntity> ProductHistories { get; set; }

    DbSet<ProductVariantEntity> ProductVariants { get; set; }

    DbSet<ProductVariantHistoryEntity> ProductVariantHistories { get; set; }

    DbSet<AddressEntity> Addresses { get; set; }

    DbSet<AllergenEntity> Allergens { get; set; }

    DbSet<InvoiceEntity> Invoices { get; set; }

    DbSet<MaterialEntity> Materials { get; set; }

    DbSet<MaterialHistoryEntity> MaterialHistories { get; set; }

    DbSet<MessageEntity> Messages { get; set; }

    DbSet<OrderEntity> Orders { get; set; }

    DbSet<OrderProductVariantEntity> OrderProductVariants { get; set; }

    DbSet<ProductMaterialEntity> ProductMaterials { get; set; }

    DbSet<ProductMaterialHistoryEntity> ProductMaterialHistories { get; set; }

    DbSet<ProductVariantAllergenEntity> ProductVariantAllergens { get; set; }

    DbSet<StockEntity> Stocks { get; set; }

    DbSet<StockLevelEntity> StockLevels { get; set; }

    DbSet<StockTransactionEntity> StockTransactions { get; set; }

    DbSet<SupplierContactEntity> SupplierContacts { get; set; }

    DbSet<SupplierEntity> Suppliers { get; set; }

    DbSet<SupplierHistoryEntity> SupplierHistories { get; set; }

    DbSet<SupplierOrderEntity> SupplierOrders { get; set; }

    DbSet<SupplierOrderMaterialEntity> SupplierOrderMaterials { get; set; }
}