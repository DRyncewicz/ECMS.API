using ecms.Application.Handlers.Commands.EditProduct;
using ecms.Domain.Errors.Products;
using FluentValidation;

namespace ecms.Application.Handlers.Commands.CreateProduct;

public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
{
    private const int MinimalProductVariantQuantity = 1;
    private const int MinimumLengthName = 2;
    private const int MaximumLengthName = 40;
    private const int MaximumLengthDescription = 1000;

    public EditProductCommandValidator()
    {
        RuleFor(p => p.Name)
            .Length(MinimumLengthName, MaximumLengthName).WithErrorCode(ProductErrorCodes.InvalidLengthName)
            .NotEmpty().WithErrorCode(ProductErrorCodes.MissingName);
        RuleFor(p => p.Description)
            .MaximumLength(MaximumLengthDescription).WithErrorCode(ProductErrorCodes.MissingDescription)
            .NotEmpty().WithErrorCode(ProductErrorCodes.InvalidLengthDescription);
        RuleFor(p => p.CategoryId)
            .NotEmpty().WithErrorCode(ProductErrorCodes.MissingCategoryId);
        RuleFor(p => p.Vat)
            .NotEmpty().WithErrorCode(ProductErrorCodes.MissingVat);
        RuleFor(p => p.Unit)
            .NotEmpty().WithErrorCode(ProductErrorCodes.MissingAlcoholContent);
        RuleFor(p => p.AlcoholContent)
            .NotEmpty().WithErrorCode(ProductErrorCodes.MissingAlcoholContent);
        RuleFor(p => p.ProductVariants).Must(p => p.Count >= MinimalProductVariantQuantity).WithErrorCode(ProductErrorCodes.EmptyVariants);
        RuleFor(p => p.Id).NotEmpty().WithErrorCode(ProductErrorCodes.MissingId);

        RuleForEach(p => p.ProductVariants).SetValidator(new ProductVariantDtoValidator());
    }
}