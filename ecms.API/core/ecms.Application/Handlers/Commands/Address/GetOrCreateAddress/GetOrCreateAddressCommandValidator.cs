using ecms.Domain.Errors.Addresses;
using FluentValidation;

namespace ecms.Application.Handlers.Commands.Address.GetOrCreateAddress;

public class GetOrCreateAddressCommandValidator : AbstractValidator<GetOrCreateAddressCommand>
{
    private const int MaximumLengthCountry = 70;
    private const int MaximumLengthCity = 70;
    private const int MaximumLengthStreet = 100;
    private const int MaximumLengthPostalCode = 11;
    private const int MaximumLengthBuldingNumber = 8;
    private const int MaximumLengthApartmentNumber = 8;

    public GetOrCreateAddressCommandValidator()
    {
        RuleFor(p => p.Country)
            .NotEmpty().WithErrorCode(AddressErrorCodes.MissingCountry)
            .MaximumLength(MaximumLengthCountry).WithErrorCode(AddressErrorCodes.InvalidLengthCountry);

        RuleFor(p => p.City)
            .NotEmpty().WithErrorCode(AddressErrorCodes.MissingCity)
            .MaximumLength(MaximumLengthCity).WithErrorCode(AddressErrorCodes.InvalidLengthCity);

        RuleFor(p => p.Street)
            .NotEmpty().WithErrorCode(AddressErrorCodes.MissingStreet)
            .MaximumLength(MaximumLengthStreet).WithErrorCode(AddressErrorCodes.InvalidLengthStreet);

        RuleFor(p => p.PostalCode)
            .NotEmpty().WithErrorCode(AddressErrorCodes.MissingPostalCode)
            .MaximumLength(MaximumLengthPostalCode).WithErrorCode(AddressErrorCodes.InvalidLengthPostalCode);

        RuleFor(p => p.BuildingNumber)
            .NotEmpty().WithErrorCode(AddressErrorCodes.MissingBuildingNumber)
            .MaximumLength(MaximumLengthBuldingNumber).WithErrorCode(AddressErrorCodes.InvalidLengthBuildingNumber);

        RuleFor(p => p.ApartmentNumber)
            .MaximumLength(MaximumLengthApartmentNumber).WithErrorCode(AddressErrorCodes.InvalidLengthApartmentNumber);
    }
}