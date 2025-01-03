using ecms.Application.Handlers.Commands.Supplier.CreateSupplier;
using ecms.Domain.Errors.Suppliers;
using FluentValidation.TestHelper;

namespace UnitTests.Handlers.Commands.Supplier.CreateSupplier;

public class CreateSupplierCommandValidatorTests
{
    private readonly CreateSupplierCommandValidator _validator;

    public CreateSupplierCommandValidatorTests()
    {
        _validator = new CreateSupplierCommandValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        //Arrange
        var command = new CreateSupplierCommand
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
        var command = new CreateSupplierCommand
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
        var command = new CreateSupplierCommand
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
        var command = new CreateSupplierCommand
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