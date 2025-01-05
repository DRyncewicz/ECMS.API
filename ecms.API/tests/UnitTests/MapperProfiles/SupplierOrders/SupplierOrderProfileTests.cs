using AutoMapper;
using ecms.Application.Handlers.Commands.SupplierOrder.CreateSupplierOrder;
using ecms.Application.Models.Dtos.SupplierOrders;
using ecms.Domain.Entities;
using FluentAssertions;
using UnitTests.Mapping;

namespace UnitTests.MapperProfiles.SupplierOrders;

public class SupplierOrderProfileTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;

    public SupplierOrderProfileTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
    }

    [Fact]
    public void Should_MapFrom_CreateSupplierOrderCommand_To_SupplierOrderEntity()
    {
        //Arrange
        var command = new CreateSupplierOrderCommand()
        {
            SupplierId = 1,
            DeliveryDate = new DateTime(2025, 9, 22, 12, 0, 0),
        };

        //Act
        var result = _mapper.Map<SupplierOrderEntity>(command);

        //Assert
        result.SupplierId.Should().Be(command.SupplierId);
        result.DeliveryDate.Should().Be(command.DeliveryDate);
        result.CreateDateTimeUtc.Should().Be(default);
        result.EditDateTimeUtc.Should().BeNull();
    }

    [Fact]
    public void Should_MapFrom_CreateSupplierOrderMaterialDto_To_SupplierOrderMaterialEntity()
    {
        //Arrange
        var command = new CreateSupplierOrderMaterialDto()
        {
            MaterialId = 1,
            Quantity = 1,
            PricePerUnit = new ecms.Domain.ValueObjects.Price(25, ecms.Domain.ValueObjects.Currency.Usd),
            Discount = 1,
        };

        //Act
        var result = _mapper.Map<SupplierOrderMaterialEntity>(command);

        //Assert
        result.MaterialId.Should().Be(command.MaterialId);
        result.Quantity.Should().Be(command.Quantity);
        result.IsDelivered.Should().Be(false);
        result.PricePerUnit.Should().Be(command.PricePerUnit);
        result.Discount.Should().Be(command.Discount);
    }

    [Fact]
    public void Should_MapFrom_SupplierOrderEntity_To_SupplierOrderDto()
    {
        //Arrange
        var command = new SupplierOrderEntity()
        {
            Id = 1,
            SupplierId = 1,
            SupplierContactId = 1,
            DeliveryDate = new DateTime(2025, 9, 22),
            CreateDateTimeUtc = new DateTimeOffset(2025, 9, 22, 12, 0, 0, TimeSpan.Zero),
            EditDateTimeUtc = new DateTimeOffset(2024, 9, 22, 12, 0, 0, 0, TimeSpan.Zero),
            Status = ecms.Domain.Enums.StatusType.Pending,
            MessageId = 1,
        };

        //Act
        var result = _mapper.Map<SupplierOrderDto>(command);

        //Assert
        result.SupplierOrderId.Should().Be(command.Id);
        result.SupplierId.Should().Be(command.SupplierId);
        result.SupplierContactId.Should().Be(command.SupplierContactId);
        result.DeliveryDate.Should().Be(command.DeliveryDate);
        result.CreateDateTimeUtc.Should().Be(command.CreateDateTimeUtc);
        result.EditDateTimeUtc.Should().Be(command.EditDateTimeUtc);
        result.Status.Should().Be(command.Status);
        result.MessageId.Should().Be(command.MessageId);
        result.SendMessage.Should().Be(true);
    }
}