using ecms.Application.Models.Dtos.Products;
using ecms.Domain.Errors.ProductVariants;
using FluentValidation;

namespace ecms.Application.Handlers.Commands.CreateProduct;

public class ProductVariantDtoValidator : AbstractValidator<ProductVariantDto>
{
    private const int MinimumLengthName = 2;
    private const int MaximumLengthName = 60;

    public ProductVariantDtoValidator()
    {
        RuleFor(p => p.Name)
            .Length(MinimumLengthName, MaximumLengthName).WithErrorCode(ProductVariantErrorCodes.InvalidLengthName)
            .NotEmpty().WithErrorCode(ProductVariantErrorCodes.MissingName);
        RuleFor(p => p.ProductId)
            .NotEmpty().WithErrorCode(ProductVariantErrorCodes.MissingProductId);
        RuleFor(p => p.Id)
            .NotNull().WithErrorCode(ProductVariantErrorCodes.MissingId);
    }
}