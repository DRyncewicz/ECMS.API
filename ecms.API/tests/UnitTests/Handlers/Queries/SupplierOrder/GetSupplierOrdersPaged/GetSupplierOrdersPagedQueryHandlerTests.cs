using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Queries.SupplierOrder;
using ecms.Domain.Entities;
using FluentAssertions;
using Moq;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Queries.SupplierOrder.GetSupplierOrdersPaged;

public class GetSupplierOrdersPagedQueryHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly GetSupplierOrdersPagedQueryHandler _handler;

    private readonly List<SupplierOrderEntity> _supplierOrders = new List<SupplierOrderEntity>()
    {
        new()
        {
            Id = 1,
            MessageId = 1,
        },
        new()
        {
            Id = 2,
            MessageId = 1,
        },
        new()
        {
            Id = 3,
            MessageId = 1,
        },
        new()
        {
            Id = 4,
            MessageId = 1,
        },
        new()
        {
            Id = 5,
            MessageId = 1,
        },
        new()
        {
            Id = 6,
            MessageId = 1,
        }
    };

    private readonly List<SupplierOrderMaterialEntity> _supplierOrderMaterials = new List<SupplierOrderMaterialEntity>()
    {
        new()
        {
            Id = 1,
            SupplierOrderId = 1,
            Quantity = 1,
            PricePerUnit = new ecms.Domain.ValueObjects.Price(10, ecms.Domain.ValueObjects.Currency.Usd)
        },
        new()
        {
            Id = 2,
            SupplierOrderId = 2,
            Quantity = 1,
            PricePerUnit = new ecms.Domain.ValueObjects.Price(10, ecms.Domain.ValueObjects.Currency.Usd)
        },
        new()
        {
            Id = 3,
            SupplierOrderId = 3,
            Quantity = 1,
            PricePerUnit = new ecms.Domain.ValueObjects.Price(10, ecms.Domain.ValueObjects.Currency.Usd)
        },
        new()
        {
            Id = 4,
            SupplierOrderId = 4,
            Quantity = 1,
            PricePerUnit = new ecms.Domain.ValueObjects.Price(10, ecms.Domain.ValueObjects.Currency.Usd)
        },
        new()
        {
            Id = 5,
            SupplierOrderId = 5,
            Quantity = 1,
            PricePerUnit = new ecms.Domain.ValueObjects.Price(10, ecms.Domain.ValueObjects.Currency.Usd)
        },
        new()
        {
            Id = 6,
            SupplierOrderId = 6,
            Quantity = 1,
            PricePerUnit = new ecms.Domain.ValueObjects.Price(10, ecms.Domain.ValueObjects.Currency.Usd)
        }
    };

    public GetSupplierOrdersPagedQueryHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _handler = new GetSupplierOrdersPagedQueryHandler(_applicationDbContext.Object, _mapper);
        var dbContextResponseSupplierOrders = _supplierOrders.AsQueryable().BuildMock();
        _applicationDbContext.Setup(p => p.SupplierOrders).Returns(dbContextResponseSupplierOrders.Object);
    }

    [Fact]
    public async Task Handle_EmptyQueryShouldReturnAllRecords()
    {
        //Arrange
        var request = new GetSupplierOrdersPagedQuery();
        foreach (var order in _supplierOrders)
        {
            order.SupplierOrderMaterials = _supplierOrderMaterials.Where(m => m.SupplierOrderId == order.Id).ToList();
        }

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        result.Value.Should().NotBeNull();
        result.Value.TotalCount.Should().Be(6);
        result.Value.SupplierOrders.Should().HaveCount(6);
    }

    [Fact]
    public async Task Handle_Pagination_OnValidRequest()
    {
        //Arrange
        var request = new GetSupplierOrdersPagedQuery()
        {
            CurrentPage = 1,
            PageSize = 2,
        };
        foreach (var order in _supplierOrders)
        {
            order.SupplierOrderMaterials = _supplierOrderMaterials.Where(p => p.SupplierOrderId == order.Id).ToList();
        }

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        result.Value.Should().NotBeNull();
        result.Value.TotalCount.Should().Be(6);
        result.Value.SupplierOrders.Should().HaveCount(2);
    }

    [Fact]
    public async Task Handle_RequestForNonExistentPage_ShouldReturnEmptyList()
    {
        //Arrange
        var request = new GetSupplierOrdersPagedQuery()
        {
            CurrentPage = 4,
            PageSize = 2,
        };

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        result.Value.Should().NotBeNull();
        result.Value.TotalCount.Should().Be(6);
        result.Value.SupplierOrders.Should().BeEmpty();
    }
}