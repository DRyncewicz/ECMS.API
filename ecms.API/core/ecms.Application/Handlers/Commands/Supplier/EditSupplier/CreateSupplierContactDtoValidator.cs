using ecms.Application.Models.Dtos.SupplierOrders;
using ecms.Domain.Errors.SupplierContacts;
using FluentValidation;

namespace ecms.Application.Handlers.Commands.EditSupplier;

public class CreateSupplierContactDtoValidator : AbstractValidator<CreateSupplierContactDto>
{
    private const int MinimumLengthName = 2;
    private const int MaximumLengthName = 150;
    private const int MaximumLengthDescription = 400;

    public CreateSupplierContactDtoValidator()
    {
        RuleFor(p => p.SupplierId).NotEmpty().WithErrorCode(SupplierContactErrorCodes.MissingSupplierId);

        RuleFor(p => p.IsActive).NotEmpty().WithErrorCode(SupplierContactErrorCodes.MissingIsActive);

        RuleFor(p => p.PhoneNumber).NotEmpty().WithErrorCode(SupplierContactErrorCodes.MissingPhoneNumber);

        RuleFor(p => p.RepresentativeName).NotEmpty().WithErrorCode(SupplierContactErrorCodes.MissingRepresentativeName)
         .Length(MinimumLengthName, MaximumLengthName).WithErrorCode(SupplierContactErrorCodes.InvalidLengthRepresentativeName);

        RuleFor(p => p.Email).NotEmpty().WithErrorCode(SupplierContactErrorCodes.MissingEmail)
         .EmailAddress().WithErrorCode(SupplierContactErrorCodes.InvalidEmail);

        RuleFor(p => p.Description).MaximumLength(MaximumLengthDescription).WithErrorCode(SupplierContactErrorCodes.InvalidLengthDescription);
    }
}