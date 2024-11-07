using ecms.Domain.Enums;
using SharedKernel;

namespace ecms.Domain.Entities;

public class ProductEntity : Entity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public int Vat { get; set; }

    public UnitType Unit { get; set; }

    public AlcoholContentType AlcoholContent { get; set; }

    public GtuCodeType? GtuCode { get; set; }

    public Guid? FileGuid { get; set; }

    public int CategoryId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<ProductHistoryEntity> ProductHistories { get; set; }

    public virtual CategoryEntity Category { get; set; }

    public virtual ICollection<ProductVariantEntity> ProductVariants { get; set; }

    public virtual ICollection<ProductMaterialEntity> ProductMaterials { get; set; }
}