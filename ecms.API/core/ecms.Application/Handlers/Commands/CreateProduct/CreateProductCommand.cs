using ecms.Application.Models.Dtos.Products;
using ecms.Domain.Enums;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.CreateProduct;

public class CreateProductCommand : IRequest<Result<int>>
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public int Vat { get; set; }

    public UnitType Unit { get; set; }

    public AlcoholContentType AlcoholContent { get; set; }

    public GtuCodeType? GtuCode { get; set; }

    public Guid? FileGuid { get; set; }
  
    public List<CreateProductVariantDto> ProductVariants { get; set; } = [];

}
