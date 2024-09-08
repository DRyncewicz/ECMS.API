using ecms.Domain.ValueObjects;
using SharedKernel;

namespace ecms.Domain.Entities;

public class ProductVariantHistoryEntity : Entity
{
    public int ProductVariantId { get; set; }

    public int ProductId { get; set; }

    public Price Price { get; set; }

    public string Name { get; set; }

    public bool IsDeleted { get; set; }

    public string CreatorUserId { get; set; }

    public DateTimeOffset CreateDateTimeUtc { get; set; }

    public virtual ProductVariantEntity ProductVariant { get; set; }
}
