using ecms.Application.Handlers.Commands.Material.CreateMaterial;
using ecms.Domain.Errors.Materials;
using FluentValidation.TestHelper;

namespace UnitTests.Handlers.Commands.Material.CreateMaterial;

public class CreateMaterialCommandValidatorTests
{
    private readonly CreateMaterialCommandValidator _validator;

    public CreateMaterialCommandValidatorTests()
    {
        _validator = new CreateMaterialCommandValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        //Arrange
        var command = new CreateMaterialCommand
        {
            Name = string.Empty
        };

        //Act
        var result = _validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorCode(MaterialErrorCodes.MissingName);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Length_Is_Less_Than_Minimum()
    {
        //Arrange
        var command = new CreateMaterialCommand
        {
            Name = "A"
        };

        //Act
        var result = _validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorCode(MaterialErrorCodes.InvalidLengthName);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Length_Is_Greater_Than_Maximum()
    {
        //Arrange
        var command = new CreateMaterialCommand
        {
            Name = new string('A', 61)
        };

        //Act
        var result = _validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorCode(MaterialErrorCodes.InvalidLengthName);
    }

    [Fact]
    public void Should_Have_Error_When_Description_Is_Empty()
    {
        //Arrange
        var command = new CreateMaterialCommand
        {
            Description = string.Empty
        };

        //Act
        var result = _validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Description)
              .WithErrorCode(MaterialErrorCodes.MissingDescription);
    }

    [Fact]
    public void Should_Have_Error_When_Description_Length_Is_Greater_Than_Maximum()
    {
        //Arrange
        var command = new CreateMaterialCommand
        {
            Description = new string('A', 1001)
        };

        //Act
        var result = _validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Description)
              .WithErrorCode(MaterialErrorCodes.InvalidLengthDescription);
    }

    [Fact]
    public void Should_Have_Error_When_UnitOfMeasure_Is_Empty()
    {
        //Arrange
        var command = new CreateMaterialCommand
        {

        };

        //Act
        var result = _validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.UnitOfMeasure)
              .WithErrorCode(MaterialErrorCodes.MissingUnitOfMeasure);
    }

    [Fact]
    public void Should_Have_Error_When_MinStockLevel_Is_Negative()
    {
        //Arrange
        var command = new CreateMaterialCommand
        {
            MinStockLevel = -1,
            MaxStockLevel = 10
        };

        //Act
        var result = _validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.MinStockLevel)
              .WithErrorCode(MaterialErrorCodes.InvalidMinStockLevel);
    }

    [Fact]
    public void Should_Have_Error_When_MinStockLevel_Is_Not_Less_Than_MaxStockLevel()
    {
        //Arrange
        var command = new CreateMaterialCommand
        {
            MinStockLevel = 10,
            MaxStockLevel = 10
        };

        //Act
        var result = _validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.MinStockLevel)
              .WithErrorCode(MaterialErrorCodes.InvalidMinStockLevel);
    }

    [Fact]
    public void Should_Have_Error_When_MaxStockLevel_Is_LowerThan_1()
    {
        //Arrange
        var command = new CreateMaterialCommand
        {
            MaxStockLevel = 0
        };

        //Act
        var result = _validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.MaxStockLevel)
              .WithErrorCode(MaterialErrorCodes.InvalidMaxStockLevel);
    }

    [Fact]
    public void Should_Have_Error_When_ReorderLevel_Is_Negative()
    {
        //Arrange
        var command = new CreateMaterialCommand
        {
            ReorderLevel = -1,
            MaxStockLevel = 10
        };

        //Act
        var result = _validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.ReorderLevel)
              .WithErrorCode(MaterialErrorCodes.InvalidReorderLevel);
    }

    [Fact]
    public void Should_Have_Error_When_ReorderLevel_Is_Not_Less_Than_MaxStockLevel()
    {
        //Arrange
        var command = new CreateMaterialCommand
        {
            ReorderLevel = 10,
            MaxStockLevel = 10
        };

        //Act
        var result = _validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.ReorderLevel)
              .WithErrorCode(MaterialErrorCodes.InvalidReorderLevel);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        //Arrange
        var command = new CreateMaterialCommand
        {
            Name = "Valid Name",
            Description = "Valid Description",
            UnitOfMeasure = ecms.Domain.Enums.UnitOfMeasureType.Grams,
            MinStockLevel = 0,
            MaxStockLevel = 10,
            ReorderLevel = 5
        };

        //Act
        var result = _validator.TestValidate(command);

        //Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
