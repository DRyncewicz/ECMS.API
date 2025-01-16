using ecms.Application.Models.Dtos.Products;

namespace ecms.Application.Models.ViewModels.Products;

public class GroupProductMaterialViewModel
{
    public List<ProductVariantMaterialGroupDto> ProductVariantMaterialGroups { get; set; } = [];
}