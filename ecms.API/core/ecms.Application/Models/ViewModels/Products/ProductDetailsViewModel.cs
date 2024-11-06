using ecms.Application.Models.Dtos.Products;
using ecms.Domain.Enums;

namespace ecms.Application.Models.ViewModels.Products;

public class ProductDetailsViewModel
{
    public int ProductId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Vat { get; set; }

    public UnitType Unit { get; set; }

    public AlcoholContentType AlcoholContent { get; set; }

    public GtuCodeType? GtuCode { get; set; }

    public Guid? FileGuid { get; set; }

    public int CategoryId { get; set; }

    public IEnumerable<ProductVariantDto> productVariantDtos { get; set; } = [];
}