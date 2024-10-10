using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Commands.EditCategory;
using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Commands.EditCategory;

public class EditCategoryCommandHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly EditCategoryCommandHandler _handler;

    public EditCategoryCommandHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _handler = new EditCategoryCommandHandler(_applicationDbContext.Object, _mapper);
        List<CategoryEntity> categories = new List<CategoryEntity>()
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
                HierarchyId = new HierarchyId("/4/"),
                Name = "Main2",
            },
            new CategoryEntity
            {
                Id = 4,
                HierarchyId = new HierarchyId("/4/1/"),
                Name = "Main2",
            },
        };
        _applicationDbContext.Setup(p => p.Categories).Returns(categories.AsQueryable().BuildMock().Object);
    }

    [Fact]
    public async Task Handle_ShouldEditCategory_If_AncestorHasChanged()
    {
        //Arrange
        var request = new EditCategoryCommand()
        {
            Name = "Name",
            CategoryId = 2,
            AncestorHierarchyId = new HierarchyId("/4/")
        };

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        _applicationDbContext.Verify(p => p.Categories.Update(It.IsAny<CategoryEntity>()), Times.Once());
    }

    [Fact]
    public async Task Handle_ShouldEditCategory_If_AncestorHasntChanged()
    {
        //Arrange
        var request = new EditCategoryCommand()
        {
            Name = "Name",
            CategoryId = 2,
            AncestorHierarchyId = new HierarchyId("/1/"),
        };       
                         
        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        _applicationDbContext.Verify(p => p.Categories.Update(It.IsAny<CategoryEntity>()), Times.Once());
    }
}
