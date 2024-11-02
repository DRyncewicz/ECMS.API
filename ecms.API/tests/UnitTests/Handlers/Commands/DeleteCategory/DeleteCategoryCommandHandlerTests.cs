using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Commands.DeleteCategory;
using ecms.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTests.Handlers.Commands.DeleteCategory;

public class DeleteCategoryCommandHandlerTests
{
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly DeleteCategoryCommandHandler _handler;

    public DeleteCategoryCommandHandlerTests()
    {
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _handler = new DeleteCategoryCommandHandler(_applicationDbContext.Object);
        List<CategoryEntity> categories = new List<CategoryEntity>()
        {
            new CategoryEntity
            {
                Id = 1,
                HierarchyId = new HierarchyId(),
                Name = "Main",
            },
            new CategoryEntity
            {
                Id = 2,
                HierarchyId = new HierarchyId("/1/"),
                Name = "Main2",
            },
            new()
            {
                Id = 3,
                HierarchyId = new HierarchyId("/1/2/"),
                Name = "Main3",
                Products = [new ProductEntity()
                {
                    IsDeleted = true
                }]
            },
             new CategoryEntity
            {
                Id = 4,
                HierarchyId = new HierarchyId("/1/3/"),
                Name = "Main2",
                Products = [new ProductEntity()
                {
                    IsDeleted = false,
                }]
            },
        };
        _applicationDbContext.Setup(p => p.Categories).Returns(categories.AsQueryable().BuildMock().Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnResultMessage_IfCategoryExists_AndContainsLinkedProduct()
    {
        //Arrange
        var request = new DeleteCategoryCommand(4);

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        result.Value.Should().Be("Unable to delete category because there are active products in that category, first delete the products or change their categories");
    }

    [Fact]
    public async Task Handle_ShouldDeleteCategory_IfThereAreNoChildren()
    {
        //Arrange
        var request = new DeleteCategoryCommand(3);

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        result.Value.Should().Be("");
    }

    [Fact]
    public async Task Handle_ShouldReturnErrorString_IfThereAreChildren()
    {
        //Arrange
        var request = new DeleteCategoryCommand(1);

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        result.Value.Should().Be("Unable to delete category because there are sub categories, delete or change parent categories first");
    }

    [Fact]
    public async Task Handle_ShouldReturnException_IfCategoryIsNotFound()
    {
        //Arrange
        var request = new DeleteCategoryCommand(999);

        // Act
        Func<Task> act = async () => await _handler.Handle(request, default);

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }
}