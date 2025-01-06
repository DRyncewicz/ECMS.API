using AutoMapper;
using ecms.Application.Handlers.Commands.SupplierOrder.CreateSupplierOrder;
using ecms.Application.Models.Dtos.SupplierOrders;
using ecms.Application.Models.ViewModels.SupplierOrders;
using ecms.Domain.Entities;
using ecms.Domain.ValueObjects;
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

    [Fact]
    public void Should_MapFrom_SupplierOrderMaterialEntity_To_SupplierOrderMaterialDto()
    {
        //Arrange
        var command = new SupplierOrderMaterialEntity()
        {
            Id = 1,
            SupplierOrderId = 1,
            Material = new()
            {
                Id = 1,
                Name = "DodasekGrubasek",
                MaxStockLevel = 7,
                MinStockLevel = 1,
                FileGuid = Guid.NewGuid(),
                Description = "Description",
                ReorderLevel = 1,
                StockLevel = new StockLevelEntity()
                {
                    StockId = 1,
                }
            },
            Quantity = 41,
            PricePerUnit = new Price(10, Currency.Usd),
            IsDelivered = true
        };

        //Act
        var result = _mapper.Map<SupplierOrderMaterialDto>(command);

        //Assert
        result.SupplierOrderMaterialId.Should().Be(command.Id);
        result.SupplierOrderId.Should().Be(command.SupplierOrderId);
        result.Material.MaterialId.Should().Be(command.Material.Id);
        result.Material.Name.Should().Be(command.Material.Name);
        result.Material.MaxStockLevel.Should().Be(command.Material.MaxStockLevel);
        result.Material.MinStockLevel.Should().Be(command.Material.MinStockLevel);
        result.Material.ReorderLevel.Should().Be(command.Material.ReorderLevel);
        result.Material.FileGuid.Should().Be(command.Material.FileGuid);
        result.Material.Description.Should().Be(command.Material.Description);
        result.Material.StockId.Should().Be(command.Material.StockLevel.StockId);
        result.Quantity.Should().Be(command.Quantity);
        result.PricePerUnit.Should().Be(command.PricePerUnit);
        result.IsDelivered.Should().Be(command.IsDelivered);
    }

    [Fact]
    public void Should_MapFrom_SupplierOrderEntity_To_SupplierOrderDetailsViewModel()
    {
        //Arrange
        var command = new SupplierOrderEntity()
        {
            Id = 1,
            DeliveryDate = new DateTime(2025, 9, 22),
            CreateDateTimeUtc = new DateTimeOffset(2025, 9, 22, 12, 0, 0, TimeSpan.Zero),
            EditDateTimeUtc = new DateTimeOffset(2024, 9, 22, 12, 0, 0, 0, TimeSpan.Zero),
            Status = ecms.Domain.Enums.StatusType.Pending,
            MessageId = 1,
            Supplier = new SupplierEntity(),
            SupplierContact = new SupplierContactEntity(),
            SupplierOrderMaterials = new List<SupplierOrderMaterialEntity>()
        };

        //Act
        var result = _mapper.Map<SupplierOrderDetailsViewModel>(command);

        //Assert
        result.SupplierOrderId.Should().Be(command.Id);
        result.DeliveryDate.Should().Be(command.DeliveryDate);
        result.CreateDateTimeUtc.Should().Be(command.CreateDateTimeUtc);
        result.EditDateTimeUtc.Should().Be(command.EditDateTimeUtc);
        result.Status.Should().Be(command.Status);
        result.MessageId.Should().Be(command.MessageId);
        result.SendMessage.Should().Be(true);
        result.SupplierOrderMaterialDtos.Should().BeEmpty();
        result.SupplierContactDto.Should().NotBeNull();
        result.SupplierDto.Should().NotBeNull();
    }
}