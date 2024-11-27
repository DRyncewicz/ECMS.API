using ecms.Domain.Errors.Categories;
using FluentValidation;

namespace ecms.Application.Handlers.Commands.EditCategory;

public class EditCategoryCommandValidator : AbstractValidator<EditCategoryCommand>
{
    private const int MinimumLengthName = 2;
    private const int MaximumLengthName = 40;

    public EditCategoryCommandValidator()
    {
        RuleFor(p => p.CategoryId)
            .NotEmpty().WithErrorCode(CategoryErrorCodes.MissingId);

        RuleFor(p => p.Name)
            .Length(MinimumLengthName, MaximumLengthName).WithErrorCode(CategoryErrorCodes.InvalidLengthName)
            .NotEmpty().WithErrorCode(CategoryErrorCodes.MissingName);

        RuleFor(p => p.AncestorHierarchyId)
            .NotEmpty().WithErrorCode(CategoryErrorCodes.MissingAncestorHierarchyId);
    }
}