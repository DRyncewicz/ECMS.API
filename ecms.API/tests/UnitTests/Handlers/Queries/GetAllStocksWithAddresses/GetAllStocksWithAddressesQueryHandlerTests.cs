using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Queries.GetAllStocksWithAddresses;
using ecms.Domain.Entities;
using FluentAssertions;
using Moq;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Queries.GetAllStocksWithAddresses;

public class GetAllStocksWithAddressesQueryHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly GetAllStocksWithAddressesQueryHandler _handler;

    public GetAllStocksWithAddressesQueryHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _handler = new GetAllStocksWithAddressesQueryHandler(_applicationDbContext.Object, _mapper);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenStocksExist()
    {
        //Arrange
        var stocks = new List<StockEntity>
        {
            new StockEntity()
            {
                Id = 1,
                Name = "Dupa",
                AddressId = 1,
                Description = "Dupa",
                IsDeleted = false,
            },
            new StockEntity()
            {
                Id = 2,
                Name = "Dupa2",
                AddressId = 2,
                Description = "Dupa2",
                IsDeleted = false,
            },
            new StockEntity()
            {
                Id = 3,
                Name = "Dupa3",
                AddressId = 3,
                Description = "Dupa3",
                IsDeleted = false,
            },
        };
        var dbContextResponse = stocks.AsQueryable().BuildMock();
        _applicationDbContext.Setup(p => p.Stocks).Returns(dbContextResponse.Object);

        var query = new GetAllStocksWithAddressesQuery();

        // Act
        var result = await _handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Stocks.Should().HaveCount(3);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenNoStocksExist()
    {
        // Arrange
        var emptyStocks = new List<StockEntity>();
        var dbContextResponse = emptyStocks.AsQueryable().BuildMock();
        _applicationDbContext.Setup(p => p.Stocks).Returns(dbContextResponse.Object);

        var query = new GetAllStocksWithAddressesQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Stocks.Should().BeEmpty();
    }
}