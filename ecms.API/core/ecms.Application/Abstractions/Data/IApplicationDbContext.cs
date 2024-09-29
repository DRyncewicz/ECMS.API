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
}