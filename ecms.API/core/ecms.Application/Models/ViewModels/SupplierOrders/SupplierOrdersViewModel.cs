using ecms.Application.Models.Dtos.SupplierOrders;

namespace ecms.Application.Models.ViewModels.SupplierOrders;

public class SupplierOrdersViewModel

{
    public IEnumerable<SupplierOrderDto> SupplierOrders { get; set; } = [];

    public int TotalCount { get; set; }
}