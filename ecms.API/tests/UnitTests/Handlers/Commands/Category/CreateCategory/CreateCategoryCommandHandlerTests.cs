using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Commands.Category.CreateCategory;
using ecms.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Data;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Commands.CreateCategory;

public class CreateCategoryCommandHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly CreateCategoryCommandHandler _handler;
    private readonly Mock<IDbTransaction> _transaction;

    public CreateCategoryCommandHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _handler = new CreateCategoryCommandHandler(_applicationDbContext.Object, _mapper);
        _transaction = new Mock<IDbTransaction>();
        _applicationDbContext.Setup(p => p.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(_transaction.Object);
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
                Name = "Main3"
            }
        };
        _applicationDbContext.Setup(p => p.Categories).Returns(categories.AsQueryable().BuildMock().Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateCategory()
    {
        //Arrange
        var request = new CreateCategoryCommand()
        {
            Name = "name",
            AncestorHierarchyId = new HierarchyId(),
            FileGuid = Guid.NewGuid()
        };

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        _transaction.Verify(p => p.Commit(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldRollBackTransaction_WhenExceptionOccurs()
    {
        // Arrange
        var request = new CreateCategoryCommand();

        _applicationDbContext.Setup(p => p.Categories.AddAsync(It.IsAny<CategoryEntity>(), It.IsAny<CancellationToken>()))
                             .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, default);

        // Assert
        await act.Should().ThrowAsync<Exception>();
        _transaction.Verify(p => p.Rollback(), Times.Once);
    }
}