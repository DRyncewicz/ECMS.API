using ecms.Application.Models.Dtos.Suppliers;

namespace ecms.Application.Models.ViewModels.Suppliers;

public class PagedSupplierViewModel
{
    public int TotalCount { get; set; }

    public List<SupplierDto> Suppliers { get; set; } = [];
}