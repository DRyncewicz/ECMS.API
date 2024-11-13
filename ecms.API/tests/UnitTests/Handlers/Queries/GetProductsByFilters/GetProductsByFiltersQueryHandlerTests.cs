using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Queries.GetProductsByFilters;
using ecms.Domain.Entities;
using FluentAssertions;
using Moq;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Queries.GetProductsByFilters;

public class GetProductsByFiltersQueryHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly GetMaterialsByDetailsQueryHandler _handler;

    private readonly List<ProductEntity> _products = new List<ProductEntity>
        {
            new ProductEntity { Id = 1, Name = "Test1", CategoryId = 1},
            new ProductEntity { Id = 2, Name = "Test2", CategoryId = 1},
            new ProductEntity { Id = 3, Name = "Test3", CategoryId = 2},
            new ProductEntity { Id = 4, Name = "Test4", CategoryId = 2}
        };

    public GetProductsByFiltersQueryHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _handler = new GetMaterialsByDetailsQueryHandler(_applicationDbContext.Object, _mapper);
        var dbContextResponse = _products.AsQueryable().BuildMock();
        _applicationDbContext.Setup(p => p.Products).Returns(dbContextResponse.Object);
    }

    [Fact]
    public async Task Handle_EmptyQueryShouldReturnAllRecords()
    {
        //Arrange
        var request = new GetProductsByFiltersQuery();

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        result.Value.TotalCount.Should().Be(4);
        result.Value.Products.Should().HaveCount(4);
    }

    [Fact]
    public async Task Handle_ShouldReturnRecordWithAllFilters()
    {
        //Arrange
        var request = new GetProductsByFiltersQuery()
        {
            Name = "Test1",
            CategoryId = 1,
            CurrentPage = 1,
            PageSize = 5
        };

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        result.Value.TotalCount.Should().Be(1);
        result.Value.Products.Should().HaveCount(1);
    }

    [Fact]
    public async Task Handle_ShouldReturnRecordWithName()
    {
        //Arrange
        var request = new GetProductsByFiltersQuery()
        {
            Name = "Test1"
        };

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        result.Value.Products.First().Name.Should().Be("Test1");
        result.Value.TotalCount.Should().Be(1);
        result.Value.Products.Should().HaveCount(1);
    }

    [Fact]
    public async Task Handle_ShouldReturnRecordWithCategoryId()
    {
        //Arrange
        var request = new GetProductsByFiltersQuery()
        {
            CategoryId = 1,
        };

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        result.Value.Products.Should().OnlyContain(x => x.CategoryId == 1);
        result.Value.TotalCount.Should().Be(2);
        result.Value.Products.Should().HaveCount(2);
    }

    [Fact]
    public async Task Handle_ShouldReturnRecordWithPagination()
    {
        //Arrange
        var request = new GetProductsByFiltersQuery()
        {
            CurrentPage = 1,
            PageSize = 2
        };

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        result.Value.Products.Should().HaveCount(2);
        result.Value.TotalCount.Should().Be(4);
    }
}