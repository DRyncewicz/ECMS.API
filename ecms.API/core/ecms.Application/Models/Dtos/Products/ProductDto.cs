namespace ecms.Application.Models.Dtos.Products;

public class ProductDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public IEnumerable<ProductVariantDto> ProductVariants { get; set; } = [];

    public int Vat { get; set; }

    public Guid? FileGuid { get; set; }
}