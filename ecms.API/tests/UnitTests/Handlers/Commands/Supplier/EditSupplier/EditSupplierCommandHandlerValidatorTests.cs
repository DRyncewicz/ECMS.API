using ecms.Application.Handlers.Commands.EditSupplier;
using ecms.Domain.Errors.Suppliers;
using FluentValidation.TestHelper;

namespace UnitTests.Handlers.Commands.Supplier.EditSupplier;

public class EditSupplierCommandValidatorTests
{
    private readonly EditSupplierCommandValidator _validator;

    public EditSupplierCommandValidatorTests()
    {
        _validator = new EditSupplierCommandValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        //Arrange
        var command = new EditSupplierCommand
        {
            Name = string.Empty
        };

        //Act
        var result = _validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorCode(SupplierErrorCodes.MissingName);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Length_Is_Less_Than_Minimum()
    {
        //Arrange
        var command = new EditSupplierCommand
        {
            Name = "A"
        };

        //Act
        var result = _validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorCode(SupplierErrorCodes.InvalidLengthName);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Length_Is_Greater_Than_Maximum()
    {
        //Arrange
        var command = new EditSupplierCommand
        {
            Name = new string('A', 151)
        };

        //Act
        var result = _validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorCode(SupplierErrorCodes.InvalidLengthName);
    }

    [Fact]
    public void Should_Have_Error_When_AddressId_Is_Empty()
    {
        //Arrange
        var command = new EditSupplierCommand
        {
            Name = "Name"
        };

        //Act
        var result = _validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.AddressId)
              .WithErrorCode(SupplierErrorCodes.MissingAddressId);
    }
}