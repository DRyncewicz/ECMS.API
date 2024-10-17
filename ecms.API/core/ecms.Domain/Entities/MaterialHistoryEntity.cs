using ecms.Domain.Enums;
using SharedKernel;

namespace ecms.Domain.Entities;

public class MaterialHistoryEntity : Entity
{
    public int MaterialId { get; set; }

    public string Name { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public UnitOfMeasureType UnitOfMeasure { get; set; }

    public string Description { get; set; }

    public double MinStockLevel { get; set; }

    public double MaxStockLevel { get; set; }

    public double ReorderLevel { get; set; }

    public Guid? FileGuid { get; set; }

    public virtual MaterialEntity Material { get; set; }
}
