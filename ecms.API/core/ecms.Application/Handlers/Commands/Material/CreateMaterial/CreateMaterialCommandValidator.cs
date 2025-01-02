using ecms.Domain.Errors.Materials;
using FluentValidation;

namespace ecms.Application.Handlers.Commands.Material.CreateMaterial;

public class CreateMaterialCommandValidator : AbstractValidator<CreateMaterialCommand>
{
    private const int MinimumLengthName = 2;
    private const int MaximumLengthName = 60;
    private const int MaximumLengthDescription = 1000;
    public CreateMaterialCommandValidator()
    {
        RuleFor(p => p.Name)
            .Length(MinimumLengthName, MaximumLengthName).WithErrorCode(MaterialErrorCodes.InvalidLengthName)
            .NotEmpty().WithErrorCode(MaterialErrorCodes.MissingName);

        RuleFor(p => p.Description)
            .MaximumLength(MaximumLengthDescription).WithErrorCode(MaterialErrorCodes.InvalidLengthDescription)
            .NotEmpty().WithErrorCode(MaterialErrorCodes.MissingDescription);

        RuleFor(p => p.UnitOfMeasure)
            .NotEmpty().WithErrorCode(MaterialErrorCodes.MissingUnitOfMeasure);

        RuleFor(p => p.MinStockLevel)
            .GreaterThanOrEqualTo(0).WithErrorCode(MaterialErrorCodes.InvalidMinStockLevel)
            .LessThan(p => p.MaxStockLevel).WithErrorCode(MaterialErrorCodes.InvalidMinStockLevel);

        RuleFor(p => p.MaxStockLevel)
            .GreaterThan(0).WithErrorCode(MaterialErrorCodes.InvalidMaxStockLevel);

        RuleFor(p => p.ReorderLevel)
            .GreaterThanOrEqualTo(0).WithErrorCode(MaterialErrorCodes.InvalidReorderLevel)
            .LessThan(p => p.MaxStockLevel).WithErrorCode(MaterialErrorCodes.InvalidReorderLevel);
    }
}
