using ecms.Domain.Errors.Categories;
using FluentValidation;

namespace ecms.Application.Handlers.Commands.GetOrCreateAddress;

public class GetOrCreateAddressCommandValidator : AbstractValidator<GetOrCreateAddressCommand>
{
    private const int MaximumLengthCountry = 70;
    private const int MaximumLengthCity = 70;
    private const int MaximumLengthStreet = 100;
    private const int MaximumLengthPostalcode = 11;
    private const int MaximumLengthBuldingnumber = 8;
    private const int MaximumLengthApartmentnumber = 8;

    public GetOrCreateAddressCommandValidator()
    {
        RuleFor(p => p.Country)
            .NotEmpty().WithErrorCode(AddressErrorCodes.MissingCountry)
            .MaximumLength(MaximumLengthCountry).WithErrorCode(AddressErrorCodes.InvalidLengthCountry);
        RuleFor(p => p.City)
            .NotEmpty().WithErrorCode(AddressErrorCodes.MissingCity)
            .MaximumLength(MaximumLengthCountry).WithErrorCode(AddressErrorCodes.InvalidLengthCity);
        RuleFor(p => p.Street)
            .NotEmpty().WithErrorCode(AddressErrorCodes.MissingStreet)
            .MaximumLength(MaximumLengthCountry).WithErrorCode(AddressErrorCodes.InvalidLengthStreet);
        RuleFor(p => p.PostalCode)
            .NotEmpty().WithErrorCode(AddressErrorCodes.MissingPostalCode)
            .MaximumLength(MaximumLengthCountry).WithErrorCode(AddressErrorCodes.InvalidLengthPostalCode);
        RuleFor(p => p.BuildingNumber)
            .NotEmpty().WithErrorCode(AddressErrorCodes.MissingBuildingNumber)
            .MaximumLength(MaximumLengthCountry).WithErrorCode(AddressErrorCodes.InvalidLengthBuildingNumber);
        RuleFor(p => p.ApartmentNumber)          
            .MaximumLength(MaximumLengthCountry).WithErrorCode(AddressErrorCodes.InvalidLengthApartmentNumber);
    }
}