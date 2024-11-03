using SharedKernel;

namespace ecms.Domain.Entities;

public class OrderProductVariantEntity : Entity
{
    public int ProductVariantId { get; set; }

    public int Quantity { get; set; }

    public int OrderId { get; set; }

    public virtual OrderEntity Order { get; set; }

    public virtual ProductVariantEntity ProductVariant { get; set; }
}