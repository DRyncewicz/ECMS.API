using ecms.Application.Handlers.Commands.EditCategory;
using ecms.Domain.Errors.Categories;
using FluentValidation.TestHelper;

namespace UnitTests.Handlers.Commands.CreateCategory;

public class EditCategoryCommandValidatorTests
{
    private readonly EditCategoryCommandValidator _validator;

    public EditCategoryCommandValidatorTests()
    {
        _validator = new EditCategoryCommandValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        // Arrange
        var command = new EditCategoryCommand
        {
            AncestorHierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId(),
            Name = string.Empty,
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name)
              .WithErrorCode(CategoryErrorCodes.MissingName);
        result.ShouldHaveValidationErrorFor(c => c.CategoryId)
              .WithErrorCode(CategoryErrorCodes.MissingId);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Too_Short()
    {
        // Arrange
        var command = new EditCategoryCommand
        {
            AncestorHierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId(),
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
        var command = new EditCategoryCommand
        {
            AncestorHierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId(),
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
        var command = new EditCategoryCommand
        {
            AncestorHierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId(),
            Name = "Valid Name",
            CategoryId = 1
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}