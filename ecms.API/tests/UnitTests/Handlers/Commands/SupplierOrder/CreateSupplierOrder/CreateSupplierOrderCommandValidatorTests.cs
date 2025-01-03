using ecms.Application.Handlers.Commands.SupplierOrder.CreateSupplierOrder;
using ecms.Application.Models.Dtos.SupplierOrders;
using ecms.Domain.Errors.SupplierOrders;
using FluentValidation.TestHelper;
using Moq;
using SharedKernal;

namespace UnitTests.Handlers.Commands.SupplierOrder.CreateSupplierOrder;

public class CreateSupplierOrderCommandValidatorTests
{
    private readonly CreateSupplierOrderCommandValidator _validator;
    private readonly Mock<IDateTimeProvider> _dateTimeProvider;

    public CreateSupplierOrderCommandValidatorTests()
    {
        _dateTimeProvider = new Mock<IDateTimeProvider>();
        _validator = new CreateSupplierOrderCommandValidator(_dateTimeProvider.Object);
        _dateTimeProvider.Setup(p => p.UtcNow).Returns(new DateTime(2025, 9, 22, 12, 0, 0));
    }

    [Fact]
    public void Should_Have_Error_When_DeliveryDate_IsInvalid()
    {
        //Arrange
        var command = new CreateSupplierOrderCommand()
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
        var command = new CreateSupplierOrderCommand()
        {
            SupplierId = 0
        };

        //Act
        var result = _validator.TestValidate(command);

        //Assert
        result.ShouldHaveValidationErrorFor(p => p.SupplierId).WithErrorCode(SupplierOrderErrorCodes.MissingSupplierId);
    }

    [Fact]
    public void Should_Have_Error_When_Language_IsEmpty()
    {
        //Arrange
        var command = new CreateSupplierOrderCommand()
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
        var command = new CreateSupplierOrderCommand()
        {
            SupplierOrderMaterialDtos = new List<CreateSupplierOrderMaterialDto>()
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
        result.ShouldHaveValidationErrorFor("SupplierOrderMaterialDtos[0].MaterialId").WithErrorCode(SupplierOrderErrorCodes.MissingMaterialId);
    }

    [Fact]
    public void Should_Have_Error_When_Quantity_IsEmpty()
    {
        //Arrange
        var command = new CreateSupplierOrderCommand()
        {
            SupplierOrderMaterialDtos = new List<CreateSupplierOrderMaterialDto>()
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
        result.ShouldHaveValidationErrorFor("SupplierOrderMaterialDtos[0].Quantity").WithErrorCode(SupplierOrderErrorCodes.InvalidQuantity);
    }

    [Fact]
    public void Should_Not_Have_Errors()
    {
        //Arrange
        var command = new CreateSupplierOrderCommand()
        {
            DeliveryDate = _dateTimeProvider.Object.UtcNow.AddHours(2),
            SupplierId = 1,
            Language = ecms.Domain.Enums.LanguageType.English,
            SupplierOrderMaterialDtos = new List<CreateSupplierOrderMaterialDto>()
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