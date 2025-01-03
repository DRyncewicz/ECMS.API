namespace ecms.Application.Handlers.Commands.Stock.EditStock;

public class EditStockRequest
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int AddressId { get; set; }
}