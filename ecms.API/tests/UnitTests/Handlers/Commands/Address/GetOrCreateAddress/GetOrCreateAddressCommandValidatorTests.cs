using ecms.Application.Handlers.Commands.Address.GetOrCreateAddress;
using ecms.Domain.Errors.Addresses;
using FluentValidation.TestHelper;

namespace UnitTests.Handlers.Commands.Address.GetOrCreateAddress;

public class GetOrCreateAddressCommandValidatorTests
{
    private readonly GetOrCreateAddressCommandValidator _validator;

    public GetOrCreateAddressCommandValidatorTests()
    {
        _validator = new GetOrCreateAddressCommandValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Country_Is_Empty()
    {
        // Arrange
        var command = new GetOrCreateAddressCommand()
        {
            Country = "",
            City = "Koszalin",
            Street = "Rodła",
            PostalCode = "75-361",
            BuildingNumber = "42",
            ApartmentNumber = "12",
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Country)
              .WithErrorCode(AddressErrorCodes.MissingCountry);
    }

    [Fact]
    public void Should_Have_Error_When_Country_Is_Too_Long()
    {
        // Arrange
        var command = new GetOrCreateAddressCommand()
        {
            Country = "DupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupa",
            City = "Koszalin",
            Street = "Rodła",
            PostalCode = "75-361",
            BuildingNumber = "42",
            ApartmentNumber = "12",
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Country)
              .WithErrorCode(AddressErrorCodes.InvalidLengthCountry);
    }

    [Fact]
    public void Should_Have_Error_When_City_Is_Empty()
    {
        // Arrange
        var command = new GetOrCreateAddressCommand()
        {
            Country = "Poland",
            City = "",
            Street = "Rodła",
            PostalCode = "75-361",
            BuildingNumber = "42",
            ApartmentNumber = "12",
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.City)
              .WithErrorCode(AddressErrorCodes.MissingCity);
    }

    [Fact]
    public void Should_Have_Error_When_City_Is_Too_Long()
    {
        // Arrange
        var command = new GetOrCreateAddressCommand()
        {
            Country = "Poland",
            City = "DupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupa",
            Street = "Rodła",
            PostalCode = "75-361",
            BuildingNumber = "42",
            ApartmentNumber = "12",
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.City)
              .WithErrorCode(AddressErrorCodes.InvalidLengthCity);
    }

    [Fact]
    public void Should_Have_Error_When_Street_Is_Empty()
    {
        // Arrange
        var command = new GetOrCreateAddressCommand()
        {
            Country = "Poland",
            City = "Koszalin",
            Street = "",
            PostalCode = "75-361",
            BuildingNumber = "42",
            ApartmentNumber = "12",
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Street)
              .WithErrorCode(AddressErrorCodes.MissingStreet);
    }

    [Fact]
    public void Should_Have_Error_When_Street_Is_Too_Long()
    {
        // Arrange
        var command = new GetOrCreateAddressCommand()
        {
            Country = "Poland",
            City = "Koszalin",
            Street = "DupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupaDupa",
            PostalCode = "75-361",
            BuildingNumber = "42",
            ApartmentNumber = "12",
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Street)
              .WithErrorCode(AddressErrorCodes.InvalidLengthStreet);
    }

    [Fact]
    public void Should_Have_Error_When_PostalCode_Is_Empty()
    {
        // Arrange
        var command = new GetOrCreateAddressCommand()
        {
            Country = "Poland",
            City = "Koszalin",
            Street = "Rodła",
            PostalCode = "",
            BuildingNumber = "42",
            ApartmentNumber = "12",
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.PostalCode)
              .WithErrorCode(AddressErrorCodes.MissingPostalCode);
    }

    [Fact]
    public void Should_Have_Error_When_PostalCode_Is_Too_Long()
    {
        // Arrange
        var command = new GetOrCreateAddressCommand()
        {
            Country = "Poland",
            City = "Koszalin",
            Street = "Rodła",
            PostalCode = "75-361-238723-21272",
            BuildingNumber = "42",
            ApartmentNumber = "12",
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.PostalCode)
              .WithErrorCode(AddressErrorCodes.InvalidLengthPostalCode);
    }

    [Fact]
    public void Should_Have_Error_When_BuildingNumber_Is_Empty()
    {
        // Arrange
        var command = new GetOrCreateAddressCommand()
        {
            Country = "Poland",
            City = "Koszalin",
            Street = "Rodła",
            PostalCode = "75-361",
            BuildingNumber = "",
            ApartmentNumber = "12",
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.BuildingNumber)
              .WithErrorCode(AddressErrorCodes.MissingBuildingNumber);
    }

    [Fact]
    public void Should_Have_Error_When_BuildingNumber_Is_Too_Long()
    {
        // Arrange
        var command = new GetOrCreateAddressCommand()
        {
            Country = "Poland",
            City = "Koszalin",
            Street = "Rodła",
            PostalCode = "75-361",
            BuildingNumber = "423243243243242",
            ApartmentNumber = "12",
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.BuildingNumber)
              .WithErrorCode(AddressErrorCodes.InvalidLengthBuildingNumber);
    }

    [Fact]
    public void Should_Have_Error_When_ApartmentNumber_Is_Too_Long()
    {
        // Arrange
        var command = new GetOrCreateAddressCommand()
        {
            Country = "Poland",
            City = "Koszalin",
            Street = "Rodła",
            PostalCode = "75-361",
            BuildingNumber = "42",
            ApartmentNumber = "12435435432",
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.ApartmentNumber)
              .WithErrorCode(AddressErrorCodes.InvalidLengthApartmentNumber);
    }

    [Fact]
    public void Should_NotHave_Errors_When_Command_Is_Valid()
    {
        // Arrange
        var command = new GetOrCreateAddressCommand()
        {
            Country = "Poland",
            City = "Koszalin",
            Street = "Rodła",
            PostalCode = "75-361",
            BuildingNumber = "42",
            ApartmentNumber = "12",
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}