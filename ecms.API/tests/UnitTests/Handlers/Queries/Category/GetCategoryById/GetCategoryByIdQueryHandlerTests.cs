using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Queries.Category.GetCategoryById;
using ecms.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTests.Handlers.Queries.Category.GetCategoryById;

public class GetCategoryByIdQueryHandlerTests
{
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly GetCategoryByIdQueryHandler _handler;

    public GetCategoryByIdQueryHandlerTests()
    {
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _handler = new GetCategoryByIdQueryHandler(_applicationDbContext.Object);
        var categories = new List<CategoryEntity>
        {
            new CategoryEntity
            {
                Id = 1,
                HierarchyId = new HierarchyId("/1/"),
                Name = "Main",
            },
            new CategoryEntity
            {
                Id = 2,
                HierarchyId = new HierarchyId("/1/1/"),
                Name = "Main2",
            },
            new CategoryEntity
            {
                Id = 3,
                HierarchyId = new HierarchyId("/"),
                Name = "Main3",
            },
        };
        var dbContextResponse = categories.AsQueryable().BuildMock();
        _applicationDbContext.Setup(p => p.Categories).Returns(dbContextResponse.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenCategoryExists()
    {
        //Arrange
        var query = new GetCategoryByIdQuery(1);

        //Act
        var result = await _handler.Handle(query, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.CategoryId.Should().Be(1);
        result.Value.Name.Should().Be("Main");
        result.Value.AncestorName.Should().Be("Main3");
    }

    [Fact]
    public async Task Handle_ShouldEarlyReturnResultFailure_WhenCategoryDoesntExist()
    {
        //Arrange
        var query = new GetCategoryByIdQuery(14);

        //Act
        var result = await _handler.Handle(query, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().NotBeNull();
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenCategoryExist_WithNoAncestor()
    {
        //Arrange
        var query = new GetCategoryByIdQuery(3);

        //Act
        var result = await _handler.Handle(query, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.CategoryId.Should().Be(3);
        result.Value.Name.Should().Be("Main3");
        result.Value.AncestorName.Should().BeEmpty();
    }
}