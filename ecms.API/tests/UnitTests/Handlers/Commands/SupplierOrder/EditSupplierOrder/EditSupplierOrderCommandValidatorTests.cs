using ecms.Application.Handlers.Commands.SupplierOrder.EditSupplierOrder;
using ecms.Application.Models.Dtos.SupplierOrders;
using ecms.Domain.Errors.SupplierOrders;
using FluentValidation.TestHelper;
using Moq;
using SharedKernal;

namespace UnitTests.Handlers.Commands.SupplierOrder.EditSupplierOrder;

public class EditSupplierOrderCommandValidatorTests
{
    private readonly EditSupplierOrderCommandValidator _validator;
    private readonly Mock<IDateTimeProvider> _dateTimeProvider;

    public EditSupplierOrderCommandValidatorTests()
    {
        _dateTimeProvider = new Mock<IDateTimeProvider>();
        _validator = new EditSupplierOrderCommandValidator(_dateTimeProvider.Object);
        _dateTimeProvider.Setup(p => p.UtcNow).Returns(new DateTime(2025, 9, 22, 12, 0, 0));
    }

    [Fact]
    public void Should_Have_Error_When_DeliveryDate_IsInvalid()
    {
        //Arrange
        var command = new EditSupplierOrderCommand()
        {
            DeliveryDate = _dateTimeProvider.Object.UtcNow.AddHours(-1)
        };

        //Act
        var result = _validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(p => p.DeliveryDate).WithErrorCode(SupplierOrderErrorCodes.InvalidDeliveryDate);
    }

    [Fact]
    public void Should_Have_Error_When_SupplierId_IsEmpty()
    {
        //Arrange
        var command = new EditSupplierOrderCommand()
        {
            SupplierId = 0
        };

        //Act
        var result = _validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(p => p.SupplierId).WithErrorCode(SupplierOrderErrorCodes.MissingSupplierId);
    }

    [Fact]
    public void Should_Have_Error_When_SupplierOrderId_IsEmpty()
    {
        //Arrange
        var command = new EditSupplierOrderCommand()
        {
            SupplierOrderId = 0
        };

        //Act
        var result = _validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(p => p.SupplierOrderId).WithErrorCode(SupplierOrderErrorCodes.MissingSupplierOrderId);
    }

    [Fact]
    public void Should_Have_Error_When_Language_IsEmpty()
    {
        //Arrange
        var command = new EditSupplierOrderCommand()
        {
            SupplierId = 1,
        };

        //Act
        var result = _validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(p => p.Language).WithErrorCode(SupplierOrderErrorCodes.MissingLanguage);
    }

    [Fact]
    public void Should_Have_Error_When_MaterialId_IsEmpty()
    {
        //Arrange
        var command = new EditSupplierOrderCommand()
        {
            EditSupplierOrderMaterialDtos = new List<EditSupplierOrderMaterialDto>()
            {
                new()
                {
                    MaterialId = 0,
                }
            }
        };

        //Act
        var result = _validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor("EditSupplierOrderMaterialDtos[0].MaterialId").WithErrorCode(SupplierOrderErrorCodes.MissingMaterialId);
    }

    [Fact]
    public void Should_Have_Error_When_Quantity_IsEmpty()
    {
        //Arrange
        var command = new EditSupplierOrderCommand()
        {
            EditSupplierOrderMaterialDtos = new List<EditSupplierOrderMaterialDto>()
            {
                new()
                {
                    Quantity = 0,
                }
            }
        };

        //Act
        var result = _validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor("EditSupplierOrderMaterialDtos[0].Quantity").WithErrorCode(SupplierOrderErrorCodes.InvalidQuantity);
    }

    [Fact]
    public void Should_Not_Have_Errors()
    {
        //Arrange
        var command = new EditSupplierOrderCommand()
        {
            DeliveryDate = _dateTimeProvider.Object.UtcNow.AddHours(2),
            SupplierId = 1,
            SupplierOrderId = 1,
            Language = ecms.Domain.Enums.LanguageType.English,
            EditSupplierOrderMaterialDtos = new List<EditSupplierOrderMaterialDto>()
            {
                new()
                {
                    MaterialId = 1,
                    Quantity = 5,
                }
            }
        };

        //Act
        var result = _validator.TestValidate(command);

        //Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}