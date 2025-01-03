using ecms.Application.Handlers.Commands.EditSupplier;
using ecms.Application.Models.Dtos.SupplierOrders;
using ecms.Domain.Errors.SupplierContacts;
using FluentValidation.TestHelper;

namespace UnitTests.Handlers.Commands.Supplier.EditSupplier
{
    public class CreateSupplierContactDtoValidatorTests
    {
        private readonly CreateSupplierContactDtoValidator _validator;

        public CreateSupplierContactDtoValidatorTests()
        {
            _validator = new CreateSupplierContactDtoValidator();
        }

        [Fact]
        public void Should_Have_Error_When_SupplierId_Is_Empty()
        {
            // Arrange
            var model = new CreateSupplierContactDto { SupplierId = 0 }; // Assuming 0 is considered empty

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.SupplierId)
                .WithErrorCode(SupplierContactErrorCodes.MissingSupplierId);
        }

        [Fact]
        public void Should_Have_Error_When_IsActive_Is_Empty()
        {
            // Arrange
            var model = new CreateSupplierContactDto { };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.IsActive)
                .WithErrorCode(SupplierContactErrorCodes.MissingIsActive);
        }

        [Fact]
        public void Should_Have_Error_When_PhoneNumber_Is_Empty()
        {
            // Arrange
            var model = new CreateSupplierContactDto { PhoneNumber = null };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber)
                .WithErrorCode(SupplierContactErrorCodes.MissingPhoneNumber);
        }

        [Fact]
        public void Should_Have_Error_When_RepresentativeName_Is_Empty()
        {
            // Arrange
            var model = new CreateSupplierContactDto { RepresentativeName = null };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.RepresentativeName)
                .WithErrorCode(SupplierContactErrorCodes.MissingRepresentativeName);
        }

        [Fact]
        public void Should_Have_Error_When_RepresentativeName_Length_Is_Invalid()
        {
            // Arrange & Act for too short name
            var modelShort = new CreateSupplierContactDto { RepresentativeName = "A" };
            var resultShort = _validator.TestValidate(modelShort);

            // Assert for too short name
            resultShort.ShouldHaveValidationErrorFor(x => x.RepresentativeName)
                .WithErrorCode(SupplierContactErrorCodes.InvalidLengthRepresentativeName);

            // Arrange & Act for too long name
            var modelLong = new CreateSupplierContactDto { RepresentativeName = new string('A', 151) };
            var resultLong = _validator.TestValidate(modelLong);

            // Assert for too long name
            resultLong.ShouldHaveValidationErrorFor(x => x.RepresentativeName)
                .WithErrorCode(SupplierContactErrorCodes.InvalidLengthRepresentativeName);
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Empty()
        {
            // Arrange
            var model = new CreateSupplierContactDto { Email = null };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorCode(SupplierContactErrorCodes.MissingEmail);
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Invalid()
        {
            // Arrange
            var model = new CreateSupplierContactDto { Email = "invalid-email" };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorCode(SupplierContactErrorCodes.InvalidEmail);
        }

        [Fact]
        public void Should_Have_No_Error_For_Valid_Model()
        {
            // Arrange
            var model = new CreateSupplierContactDto
            {
                SupplierId = 1,  // Valid SupplierId as an int.
                IsActive = true,
                IsCommon = false,
                PhoneNumber = "1234567890",
                RepresentativeName = "John Doe",
                Email = "john.doe@example.com",
                Description = "A valid description."
            };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}