using ecms.Domain.Errors.ProductMaterials;
using FluentValidation;

namespace ecms.Application.Handlers.Commands.LinkProductVariantMaterials;

public class LinkProductVariantMaterialsCommandValidator : AbstractValidator<LinkProductVariantMaterialsCommand>
{
    public LinkProductVariantMaterialsCommandValidator()
    {
        RuleFor(p => p.ProductVariantId).NotEmpty().WithErrorCode(ProductMaterialErrorCodes.MissingProductVariantId);

        RuleFor(p => p.ProductMaterialDtos).Must(p => p.Count > 0).WithErrorCode(ProductMaterialErrorCodes.InvalidProductMaterialsAmount);

        RuleForEach(p => p.ProductMaterialDtos).SetValidator(new ProductMaterialDtoValidator());
    }
}