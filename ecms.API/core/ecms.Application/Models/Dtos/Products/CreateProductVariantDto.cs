using ecms.Domain.ValueObjects;

namespace ecms.Application.Models.Dtos.Products;

public class CreateProductVariantDto
{
    public string Name { get; set; }

    public Price Price { get; set; }
}
