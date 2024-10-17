namespace ecms.Domain.Errors.Addresses;

public static class AddressErrorCodes
{
    public const string MissingCountry = nameof(MissingCountry);

    public const string InvalidLengthCountry = nameof(InvalidLengthCountry);

    public const string MissingCity = nameof(MissingCity);

    public const string InvalidLengthCity = nameof(InvalidLengthCity);

    public const string MissingStreet = nameof(MissingStreet);

    public const string InvalidLengthStreet = nameof(InvalidLengthStreet);

    public const string MissingPostalCode = nameof(MissingPostalCode);

    public const string InvalidLengthPostalCode = nameof(InvalidLengthPostalCode);

    public const string MissingBuildingNumber = nameof(MissingBuildingNumber);

    public const string InvalidLengthBuildingNumber = nameof(InvalidLengthBuildingNumber);

    public const string InvalidLengthApartmentNumber = nameof(InvalidLengthApartmentNumber);
}