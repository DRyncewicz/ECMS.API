using ecms.Application.Models.Dtos.Materials;
using ecms.Domain.Errors.Materials;
using FluentValidation;

namespace ecms.Application.Handlers.Commands.LinkProductVariantMaterials;

public class ProductMaterialDtoValidator : AbstractValidator<ProductMaterialDto>
{
    public ProductMaterialDtoValidator()
    {
        RuleFor(dto => dto.MaterialId).NotEmpty().WithErrorCode(MaterialErrorCodes.MissingId);

        RuleFor(dto => dto.Quantity).NotEmpty().WithErrorCode(MaterialErrorCodes.MissingQuantity)
            .GreaterThan(0).WithErrorCode(MaterialErrorCodes.InvalidQuantity);
    }
}