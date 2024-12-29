using ecms.Domain.Errors.Materials;
using FluentValidation;

namespace ecms.Application.Handlers.Commands.LinkProductVariantMaterials;

public class LinkProductVariantMaterialsCommandValidator : AbstractValidator<LinkProductVariantMaterialsCommand>
{
    public LinkProductVariantMaterialsCommandValidator()
    {
        RuleFor(p => p.ProductVariantId).NotEmpty().WithErrorCode(MaterialErrorCodes.MissingProductVariantId);

        RuleFor(p => p.ProductMaterialDtos).Must(p => p.Count > 0).WithErrorCode(MaterialErrorCodes.InvalidProductMaterialsAmount);

        RuleForEach(p => p.ProductMaterialDtos).SetValidator(new ProductMaterialDtoValidator());
    }
}