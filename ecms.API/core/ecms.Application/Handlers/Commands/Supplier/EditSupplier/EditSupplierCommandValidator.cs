using ecms.Domain.Errors.Suppliers;
using FluentValidation;

namespace ecms.Application.Handlers.Commands.EditSupplier;

public class EditSupplierCommandValidator : AbstractValidator<EditSupplierCommand>
{
    private const int MinimumLengthName = 2;
    private const int MaximumLengthName = 150;

    public EditSupplierCommandValidator()
    {
        RuleFor(p => p.Name)
            .Length(MinimumLengthName, MaximumLengthName).WithErrorCode(SupplierErrorCodes.InvalidLengthName)
            .NotEmpty().WithErrorCode(SupplierErrorCodes.MissingName);

        RuleFor(p => p.AddressId)
            .NotEmpty().WithErrorCode(SupplierErrorCodes.MissingAddressId);

        RuleForEach(p => p.Contacts).SetValidator(new CreateSupplierContactDtoValidator());
    }
}