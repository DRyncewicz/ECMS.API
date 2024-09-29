using SharedKernel;

namespace ecms.Domain.Entities;

public class ProductMaterialEntity : Entity
{
    public int ProductId { get; set; }

    public int MaterialId {  get; set; }

    public double Quantity { get; set; }

    public virtual ProductEntity Product { get; set; }

    public ICollection<ProductMaterialHistoryEntity> ProductMaterialHistories { get; set; }
}
