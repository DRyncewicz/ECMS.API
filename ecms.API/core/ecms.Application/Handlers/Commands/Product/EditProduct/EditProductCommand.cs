using ecms.Application.Models.Dtos.Products;
using ecms.Domain.Enums;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.EditProduct;

public class EditProductCommand : IRequest<Result<int>>
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public int Vat { get; set; }

    public UnitType Unit { get; set; }

    public AlcoholContentType AlcoholContent { get; set; }

    public GtuCodeType? GtuCode { get; set; }

    public Guid? FileGuid { get; set; }

    public List<ProductVariantDto> ProductVariants { get; set; }

    public EditProductCommand(EditProductRequest request, int id)
    {
        Id = id;
        Name = request.Name;
        Description = request.Description;
        CategoryId = request.CategoryId;
        Vat = request.Vat;
        Unit = request.Unit;
        AlcoholContent = request.AlcoholContent;
        GtuCode = request.GtuCode;
        FileGuid = request.FileGuid;
        ProductVariants = request.ProductVariants;
    }

    public EditProductCommand()
    {
    }
}