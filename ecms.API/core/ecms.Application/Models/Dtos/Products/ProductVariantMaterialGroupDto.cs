namespace ecms.Application.Models.Dtos.Products;

public class ProductVariantMaterialGroupDto
{
    public int ProductVariantId { get; set; }

    public string ProductVariantName { get; set; } = string.Empty;

    public List<ProductMaterialListItemDto> productMaterialListItems { get; set; } = [];
}