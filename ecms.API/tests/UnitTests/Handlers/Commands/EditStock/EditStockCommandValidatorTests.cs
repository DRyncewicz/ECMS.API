using ecms.Domain.Errors.Stocks;
using FluentValidation.TestHelper;

namespace ecms.Application.Handlers.Commands.EditStock.Tests
{
    public class EditStockCommandValidatorTests
    {
        private readonly EditStockCommandValidator _validator;

        public EditStockCommandValidatorTests()
        {
            _validator = new EditStockCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_StockId_Is_Empty()
        {
            // Arrange
            var command = new EditStockCommand
            {
                StockId = 0
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.StockId)
                  .WithErrorCode(StockErrorCodes.MissingId);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            // Arrange
            var command = new EditStockCommand
            {
                Name = string.Empty
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name)
                  .WithErrorCode(StockErrorCodes.MissingName);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Exceeds_Max_Length()
        {
            // Arrange
            var command = new EditStockCommand
            {
                Name = new string('A', 51)
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name)
                  .WithErrorCode(StockErrorCodes.InvalidLengthName);
        }

        [Fact]
        public void Should_Have_Error_When_Description_Exceeds_Max_Length()
        {
            // Arrange
            var command = new EditStockCommand
            {
                Description = new string('A', 501)
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Description)
                  .WithErrorCode(StockErrorCodes.InvalidLengthDescription);
        }

        [Fact]
        public void Should_Have_Error_When_AddressId_Is_Empty()
        {
            // Arrange
            var command = new EditStockCommand
            {
                AddressId = 0
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.AddressId)
                  .WithErrorCode(StockErrorCodes.MissingAddressId);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            // Arrange
            var command = new EditStockCommand
            {
                StockId = 1,
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
}