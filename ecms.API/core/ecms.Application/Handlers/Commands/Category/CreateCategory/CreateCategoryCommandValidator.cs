using ecms.Domain.Errors.Categories;
using FluentValidation;

namespace ecms.Application.Handlers.Commands.Category.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    private const int MinimumLengthName = 2;
    private const int MaximumLengthName = 40;

    public CreateCategoryCommandValidator()
    {
        RuleFor(p => p.AncestorHierarchyId)
            .NotEmpty().WithErrorCode(CategoryErrorCodes.MissingHierarchyId);
        RuleFor(p => p.Name)
            .Length(MinimumLengthName, MaximumLengthName).WithErrorCode(CategoryErrorCodes.InvalidLengthName)
            .NotEmpty().WithErrorCode(CategoryErrorCodes.MissingName);
    }
}