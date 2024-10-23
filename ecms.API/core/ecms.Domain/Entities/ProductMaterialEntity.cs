using SharedKernel;

namespace ecms.Domain.Entities;

public class ProductMaterialEntity : Entity
{
    public int ProductVariantId { get; set; }

    public int MaterialId { get; set; }

    public double Quantity { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ProductVariantEntity ProductVariant { get; set; }

    public ICollection<ProductMaterialHistoryEntity> ProductMaterialHistories { get; set; }

    public virtual MaterialEntity Material { get; set; }
}