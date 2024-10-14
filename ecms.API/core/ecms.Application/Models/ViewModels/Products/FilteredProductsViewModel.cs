using ecms.Application.Models.Dtos.Products;

namespace ecms.Application.Models.ViewModels.Products;

public class FilteredProductsViewModel
{
    public IEnumerable<ProductDto> Products { get; set; } = [];

    public int TotalCount { get; set; }
}