using ecms.Application.Handlers.Commands.CreateCategory;
using ecms.Domain.Errors.Categories;
using FluentValidation.TestHelper;

namespace UnitTests.Handlers.Commands.CreateCategory;

public class CreateCategoryCommandValidatorTests
{
    private readonly CreateCategoryCommandValidator _validator;

    public CreateCategoryCommandValidatorTests()
    {
        _validator = new CreateCategoryCommandValidator();
    }
   
    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        // Arrange
        var command = new CreateCategoryCommand
        {
            AncenstorHierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId(),
            Name = string.Empty
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name)
              .WithErrorCode(CategoryErrorCodes.MissingName);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Too_Short()
    {
        // Arrange
        var command = new CreateCategoryCommand
        {
            AncenstorHierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId(),
            Name = "A" // Less than MinimumLengthName (2)
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name)
              .WithErrorCode(CategoryErrorCodes.InvalidLengthName);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Too_Long()
    {
        // Arrange
        var command = new CreateCategoryCommand
        {
            AncenstorHierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId(),
            Name = new string('A', 41) // More than MaximumLengthName (40)
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name)
              .WithErrorCode(CategoryErrorCodes.InvalidLengthName);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        // Arrange
        var command = new CreateCategoryCommand
        {
            AncenstorHierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId(),
            Name = "Valid Name"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}