using ecms.Application.Models.Dtos.Addresses;
using ecms.Application.Models.Dtos.Stocks;

namespace ecms.Application.Models.ViewModels.Stocks;

public class StockViewModel
{
    public IEnumerable<AddressDto> Addresses { get; set; } = [];

    public IEnumerable<StockDto> Stocks { get; set; } = [];
}