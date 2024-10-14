using SharedKernel;

namespace ecms.Domain.Entities;

public class ProductMaterialHistoryEntity : Entity
{
    public int ProductMaterialId { get; set; }

    public int ProductId { get; set; }

    public int MaterialId { get; set; }

    public double Quantity { get; set; }

    public bool IsDeleted { get; set; }

    public DateTimeOffset CreateDateTimeUtc { get; set; }

    public string CreatorUsedId { get; set; }

    public virtual ProductMaterialEntity ProductMaterial { get; set; }
}