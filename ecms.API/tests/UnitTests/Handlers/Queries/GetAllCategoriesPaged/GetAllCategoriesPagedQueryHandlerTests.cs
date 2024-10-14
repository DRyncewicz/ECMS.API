using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Queries.GetAllCategoriesPaged;
using ecms.Domain.Entities;
using FluentAssertions;
using Moq;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Queries.GetAllCategoriesPaged;

public class GetAllCategoriesPagedQueryHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly GetAllCategoriesPagedQueryHandler _handler;

    private readonly List<CategoryEntity> _categories = new List<CategoryEntity>
        {
            new CategoryEntity { Id = 1, Name = "Test1", HierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId("/") },
            new CategoryEntity { Id = 2, Name = "Test2", HierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId("/1/") },
            new CategoryEntity { Id = 3, Name = "Test3", HierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId("/2/") },
            new CategoryEntity { Id = 4, Name = "Test4", HierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId("/1/20/") }
        };

    public GetAllCategoriesPagedQueryHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _handler = new GetAllCategoriesPagedQueryHandler(_applicationDbContext.Object, _mapper);
        var dbContextResponse = _categories.AsQueryable().BuildMock();
        _applicationDbContext.Setup(p => p.Categories).Returns(dbContextResponse.Object);
    }

    [Fact]
    public async Task Handle_EmptyQueryShouldReturnAllRecords()
    {
        //Arrange
        var request = new GetAllCategoriesPagedQuery() { CurrentPage = 1, PageSize = 5 };

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        result.Value.TotalCount.Should().Be(4);
        result.Value.Categories.Should().HaveCount(4);
        result.Value.Categories.FirstOrDefault(p => p.CategoryId == 1).AncestorName.Should().Be(string.Empty);
        result.Value.Categories.FirstOrDefault(p => p.CategoryId == 2).AncestorName.Should().Be("Test1");
        result.Value.Categories.FirstOrDefault(p => p.CategoryId == 3).AncestorName.Should().Be("Test1");
        result.Value.Categories.FirstOrDefault(p => p.CategoryId == 4).AncestorName.Should().Be("Test2");
    }

    [Fact]
    public async Task Handle_Pagination_OnValidRequest()
    {
        // Arrange
        var request = new GetAllCategoriesPagedQuery() { CurrentPage = 1, PageSize = 2 };

        // Act
        var result = await _handler.Handle(request, default);

        // Assert
        result.Value.Should().NotBeNull();
        result.Value.TotalCount.Should().Be(4);
        result.Value.Categories.Should().HaveCount(2);
    }

    [Fact]
    public async Task Handle_RequestForNonExistentPage_ShouldReturnEmptyList()
    {
        // Arrange
        var request = new GetAllCategoriesPagedQuery() { CurrentPage = 4, PageSize = 2 };

        // Act
        var result = await _handler.Handle(request, default);

        // Assert
        result.Value.Should().NotBeNull();
        result.Value.TotalCount.Should().Be(4);
        result.Value.Categories.Should().BeEmpty();
    }
}