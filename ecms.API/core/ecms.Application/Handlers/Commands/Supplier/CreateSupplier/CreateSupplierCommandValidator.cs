using ecms.Domain.Errors.Suppliers;
using FluentValidation;

namespace ecms.Application.Handlers.Commands.Supplier.CreateSupplier;

public class CreateSupplierCommandValidator : AbstractValidator<CreateSupplierCommand>
{
    private const int MinimumLengthName = 2;
    private const int MaximumLengthName = 150;

    public CreateSupplierCommandValidator()
    {
        RuleFor(p => p.Name)
            .Length(MinimumLengthName, MaximumLengthName).WithErrorCode(SupplierErrorCodes.InvalidLengthName)
            .NotEmpty().WithErrorCode(SupplierErrorCodes.MissingName);

        RuleFor(p => p.AddressId)
            .NotEmpty().WithErrorCode(SupplierErrorCodes.MissingAddressId);
    }
}
