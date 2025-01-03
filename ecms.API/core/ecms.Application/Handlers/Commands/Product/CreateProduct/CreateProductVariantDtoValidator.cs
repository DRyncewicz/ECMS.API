using ecms.Application.Models.Dtos.Products;
using ecms.Domain.Errors.ProductVariants;
using FluentValidation;

namespace ecms.Application.Handlers.Commands.Product.CreateProduct;

public class CreateProductVariantDtoValidator : AbstractValidator<CreateProductVariantDto>
{
    private const int MinimumLengthName = 2;
    private const int MaximumLengthName = 60;

    public CreateProductVariantDtoValidator()
    {
        RuleFor(p => p.Name)
            .Length(MinimumLengthName, MaximumLengthName).WithErrorCode(ProductVariantErrorCodes.InvalidLengthName)
            .NotEmpty().WithErrorCode(ProductVariantErrorCodes.MissingName);
    }
}