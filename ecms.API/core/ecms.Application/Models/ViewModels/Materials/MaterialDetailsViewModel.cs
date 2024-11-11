using ecms.Application.Models.Dtos.StockLevels;
using ecms.Domain.Enums;

namespace ecms.Application.Models.ViewModels.Materials;

public class MaterialDetailsViewModel
{
    public int MaterialId { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public UnitOfMeasureType UnitOfMeasure { get; set; }

    public string Description { get; set; } = string.Empty;

    public double MinStockLevel { get; set; }

    public double MaxStockLevel { get; set; }

    public double ReorderLevel { get; set; }

    public Guid? FileGuid { get; set; }

    public StockLevelDto StockLevel {  get; set; } = new StockLevelDto();
}
