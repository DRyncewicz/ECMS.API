using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Domain.Entities;
using FluentAssertions;
using Moq;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Queries.GetSuppliersPaged;

public class GetSuppliersPagedQueryHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly GetSuppliersPagedQueryHandler _handler;

    private readonly List<SupplierEntity> _suppliers = new List<SupplierEntity>
        {
            new()
            {
                Id = 1,
            },
            new()
            {
                Id = 2,
            },
            new()
            {
                Id = 3,
            },
            new()
            {
                Id = 4,
            },
            new()
            {
                Id = 5,
            },
            new()
            {
                Id = 6,
            },
        };

    public GetSuppliersPagedQueryHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _handler = new GetSuppliersPagedQueryHandler(_applicationDbContext.Object, _mapper);
        var dbContextResponse = _suppliers.AsQueryable().BuildMock();
        _applicationDbContext.Setup(p => p.Suppliers).Returns(dbContextResponse.Object);
    }

    [Fact]
    public async Task Handle_EmptyQueryShouldReturnAllRecords()
    {
        //Arrange
        var request = new GetSuppliersPagedQuery();

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        result.Value.Should().NotBeNull();
        result.Value.TotalCount.Should().Be(6);
        result.Value.Suppliers.Should().HaveCount(6);
    }

    [Fact]
    public async Task Handle_Pagination_OnValidRequest()
    {
        // Arrange
        var request = new GetSuppliersPagedQuery() { CurrentPage = 1, PageSize = 2 };

        // Act
        var result = await _handler.Handle(request, default);

        // Assert
        result.Value.Should().NotBeNull();
        result.Value.TotalCount.Should().Be(6);
        result.Value.Suppliers.Should().HaveCount(2);
    }

    [Fact]
    public async Task Handle_RequestForNonExistentPage_ShouldReturnEmptyList()
    {
        // Arrange
        var request = new GetSuppliersPagedQuery() { CurrentPage = 4, PageSize = 2 };

        // Act
        var result = await _handler.Handle(request, default);

        // Assert
        result.Value.Should().NotBeNull();
        result.Value.TotalCount.Should().Be(6);
        result.Value.Suppliers.Should().BeEmpty();
    }
}