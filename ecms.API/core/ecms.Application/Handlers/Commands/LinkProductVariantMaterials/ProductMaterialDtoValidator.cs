using ecms.Application.Models.Dtos.Materials;
using ecms.Domain.Errors.ProductMaterials;
using FluentValidation;

namespace ecms.Application.Handlers.Commands.LinkProductVariantMaterials;

public class ProductMaterialDtoValidator : AbstractValidator<ProductMaterialDto>
{
    public ProductMaterialDtoValidator()
    {
        RuleFor(dto => dto.MaterialId).NotEmpty().WithErrorCode(ProductMaterialErrorCodes.MissingMaterialId);

        RuleFor(dto => dto.Quantity).NotEmpty().WithErrorCode(ProductMaterialErrorCodes.MissingQuantity)
            .GreaterThan(0).WithErrorCode(ProductMaterialErrorCodes.InvalidQuantity);
    }
}