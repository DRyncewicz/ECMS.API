using ecms.Application.Handlers.Commands.CreateStock;
using ecms.Domain.Errors.Stocks;
using FluentValidation.TestHelper;

namespace UnitTests.Handlers.Commands.CreateStock;

public class CreateStockCommandValidatorTests
{
    private readonly CreateStockCommandValidator _validator;

    public CreateStockCommandValidatorTests()
    {
        _validator = new CreateStockCommandValidator();
    }

    [Fact]
    public void ShouldHaveErrorWhenNameIsEmpty()
    {
        // Arrange
        var command = new CreateStockCommand
        {
            Name = string.Empty,
            Description = "Dupa",
            AddressId = 1
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorCode(StockErrorCodes.MissingName);
    }

    [Fact]
    public void ShouldHaveErrorWhenNameExceedsMaximumLength()
    {
        // Arrange
        var command = new CreateStockCommand
        {
            Name = new string('a', 51),
            Description = "Dupa",
            AddressId = 1
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorCode(StockErrorCodes.InvalidLengthName);
    }

    [Fact]
    public void ShouldNotHaveErrorWhenNameIsValid()
    {
        // Arrange
        var command = new CreateStockCommand
        {
            Name = "Dupa",
            Description = "Dupa",
            AddressId = 1           
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void ShouldHaveErrorWhenDescriptionExceedsMaximumLength()
    {
        // Arrange
        var command = new CreateStockCommand
        {
            Name = "Dupa",
            Description = new string('a', 501),
            AddressId = 1
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description)
              .WithErrorCode(StockErrorCodes.InvalidLengthDescription);
    }

    [Fact]
    public void ShouldNotHaveErrorWhenDescriptionIsValid()
    {
        // Arrange
        var command = new CreateStockCommand
        {
            Name = "Dupa",
            Description = "Dupa",
            AddressId = 1
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void ShouldHaveErrorWhenAddressIdIsEmpty()
    {
        // Arrange
        var command = new CreateStockCommand
        {
            Name = "Dupa",
            Description = "Dupa",
            AddressId = 0
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.AddressId)
              .WithErrorCode(StockErrorCodes.MissingAddressId);
    }

    [Fact]
    public void ShouldNotHaveErrorWhenAddressIdIsValid()
    {
        // Arrange
        var command = new CreateStockCommand
        {
            Name = "Dupa",
            Description = "Dupa",
            AddressId = 1
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}