using ecms.Domain.Enums;

namespace ecms.Application.Handlers.Commands.Material.EditMaterial;

public class EditMaterialRequest
{
    public string Name { get; set; } = string.Empty;

    public UnitOfMeasureType UnitOfMeasure { get; set; }

    public string Description { get; set; } = string.Empty;

    public double MinStockLevel { get; set; }

    public double MaxStockLevel { get; set; }

    public bool IsActive { get; set; }

    public double ReorderLevel { get; set; }

    public Guid? FileGuid { get; set; }

    public string? BatchNumber { get; set; }

    public int StockId { get; set; }
}
