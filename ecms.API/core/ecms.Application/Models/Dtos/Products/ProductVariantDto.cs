using ecms.Application.Models.Dtos.Allergens;
using ecms.Domain.ValueObjects;

namespace ecms.Application.Models.Dtos.Products;

public class ProductVariantDto
{
    public string Name { get; set; } = string.Empty;

    public Price Price { get; set; }

    public int ProductId { get; set; }

    public int Id { get; set; }

    public List<AllergenDto> Allergens { get; set; } = [];
}