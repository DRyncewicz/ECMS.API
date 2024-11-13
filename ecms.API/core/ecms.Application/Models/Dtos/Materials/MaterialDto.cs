namespace ecms.Application.Models.Dtos.Materials;

public class MaterialDto
{
    public int MaterialId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Guid? FileGuid { get; set; }

    public double MinStockLevel { get; set; }

    public double MaxStockLevel { get; set; }

    public double ReorderLevel { get; set; }

    public int StockId { get; set; }

    public bool IsActive { get; set; }
}
