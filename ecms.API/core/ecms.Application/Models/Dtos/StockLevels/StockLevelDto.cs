namespace ecms.Application.Models.Dtos.StockLevels;

public class StockLevelDto
{
    public int StockLevelId { get; set; }

    public int StockId { get; set; }

    public int Quantity { get; set; }

    public string? BatchNumber { get; set; }

    public DateTimeOffset? LastUpdated { get; set; }
}
