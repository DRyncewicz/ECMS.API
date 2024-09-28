using ecms.Application.Models.Dtos.Products;
using ecms.Domain.Enums;

namespace ecms.Application.Handlers.Commands.EditProduct;

public class EditProductRequest
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public int Vat { get; set; }

    public UnitType Unit { get; set; }

    public AlcoholContentType AlcoholContent { get; set; }

    public GtuCodeType? GtuCode { get; set; }

    public Guid? FileGuid { get; set; }

    public List<ProductVariantDto> ProductVariants { get; set; }
}
