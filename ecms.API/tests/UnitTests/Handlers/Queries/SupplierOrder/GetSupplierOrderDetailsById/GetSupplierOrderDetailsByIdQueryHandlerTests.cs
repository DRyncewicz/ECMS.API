using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Queries.SupplierOrder.GetSupplierOrderDetailsById;
using ecms.Domain.Entities;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Queries.SupplierOrder.GetSupplierOrderDetailsByIdQueryHandlerTests;

public class GetSupplierOrderDetailsByIdQueryHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly GetSupplierOrderDetailsByIdQueryHandler _handler;

    public GetSupplierOrderDetailsByIdQueryHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _handler = new GetSupplierOrderDetailsByIdQueryHandler(_applicationDbContext.Object, _mapper);

        var supplierOrders = new List<SupplierOrderEntity>()
        {
            new()
            {
                Id = 1,
                DeliveryDate = new DateTime(2025, 9, 22),
                CreateDateTimeUtc = new DateTimeOffset(2025, 9, 22, 12, 0, 0, TimeSpan.Zero),
                EditDateTimeUtc = new DateTimeOffset(2024, 9, 22, 12, 0, 0, 0, TimeSpan.Zero),
                Status = ecms.Domain.Enums.StatusType.Pending,
                Supplier = new SupplierEntity()
                {
                    Id = 1
                },
                SupplierContact = new SupplierContactEntity()
                {
                    Id = 1
                },
                SupplierOrderMaterials = new List<SupplierOrderMaterialEntity>()
                {
                    new()
                    {
                        Id = 1,
                    }
                }
            },
        };

        var dbContextResponseSupplierOrders = supplierOrders.AsQueryable().BuildMockDbSet();
        _applicationDbContext.Setup(p => p.SupplierOrders).Returns(dbContextResponseSupplierOrders.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturn_SupplierOrderDetails()
    {
        //Arrange
        var query = new GetSupplierOrderDetailsByIdQuery(1);

        //Act
        var result = await _handler.Handle(query, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.SupplierOrderId.Should().Be(1);
        result.Value.DeliveryDate.Should().Be(new DateTime(2025, 9, 22));
        result.Value.CreateDateTimeUtc.Should().Be(new DateTimeOffset(2025, 9, 22, 12, 0, 0, TimeSpan.Zero));
        result.Value.EditDateTimeUtc.Should().Be(new DateTimeOffset(2024, 9, 22, 12, 0, 0, TimeSpan.Zero));
        result.Value.Status.Should().Be(ecms.Domain.Enums.StatusType.Pending);
        result.Value.SupplierDto.Id.Should().Be(1);
        result.Value.SupplierContactDto.Id.Should().Be(1);
        result.Value.SupplierOrderMaterialDtos.First().SupplierOrderMaterialId.Should().Be(1);
    }

    [Fact]
    public async Task Handle_ShouldReturnResultFailure_WhenSupplierOrderDoesntExist()
    {
        //Arrange
        var query = new GetSupplierOrderDetailsByIdQuery(10);

        //Act
        var result = await _handler.Handle(query, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().NotBeNull();
    }
}