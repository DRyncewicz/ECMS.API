using ecms.Application.Models.Dtos.Addresses;

namespace ecms.Application.Models.Dtos.Stocks;

public class StockDto
{
    public int StockId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public AddressDto AddressDto { get; set; } = new AddressDto();
}