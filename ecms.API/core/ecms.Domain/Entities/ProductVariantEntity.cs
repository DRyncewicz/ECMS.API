using ecms.Domain.ValueObjects;
using SharedKernel;

namespace ecms.Domain.Entities;

public class ProductVariantEntity : Entity
{
    public int ProductId { get; set; }

    public Price Price { get; set; }

    public string Name { get; set; }

    public bool IsDeleted { get; set; }
    public virtual ProductEntity Product { get; set; }

    public virtual ICollection<ProductVariantHistoryEntity> ProductVariantHistories { get; set; }
}