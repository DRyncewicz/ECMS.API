namespace ecms.Application.Models.Dtos.Products;

public class ProductMaterialListItemDto
{
    public int ProductMaterialId { get; set; }

    public int MaterialId { get; set; }

    public double Quantity { get; set; }

    public string Name { get; set; } = string.Empty;
}