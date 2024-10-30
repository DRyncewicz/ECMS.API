using AutoMapper;
using ecms.Application.Handlers.Commands.CreateCategory;
using ecms.Application.Handlers.Commands.EditCategory;
using ecms.Application.Models.Dtos.Categories;
using ecms.Domain.Entities;
using FluentAssertions;
using UnitTests.Mapping;

namespace UnitTests.MapperProfiles.Categories;

public class CategoryProfileTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;

    public CategoryProfileTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
    }

    [Fact]
    public void Should_MapFrom_CreateCategoryCommand_To_CategoryEntity()
    {
        //Arrange
        var command = new CreateCategoryCommand()
        {
            AncestorHierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId(),
            Name = "Name",
            FileGuid = Guid.NewGuid(),
        };

        //Act
        var result = _mapper.Map<CategoryEntity>(command);

        //Assert
        result.Name.Should().Be(command.Name);
        result.FileGuid.Should().Be(command.FileGuid);
        result.Id.Should().Be(0);
        result.Products.Should().BeNull();
        result.HierarchyId.Should().BeNull();
        result.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public void Should_MapFrom_EditCategoryCommand_To_CategoryEntity()
    {
        //Arrange
        var command = new EditCategoryCommand()
        {
            CategoryId = 1,
            AncestorHierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId(),
            Name = "Name",
            FileGuid = Guid.NewGuid(),
        };

        //Act
        var result = _mapper.Map<CategoryEntity>(command);

        //Assert
        result.Id.Should().Be(command.CategoryId);
        result.Name.Should().Be(command.Name);
        result.FileGuid.Should().Be(command.FileGuid);
        result.Products.Should().BeNull();
        result.HierarchyId.Should().BeNull();
        result.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public void Should_MapFrom_CategoryEntity_To_CategoryDto()
    {
        //Arrange
        var command = new CategoryEntity()
        {
            HierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId(),
            Name = "Name",
            Id = 1,
            FileGuid = Guid.NewGuid(),
        };

        //Act
        var result = _mapper.Map<CategoryDto>(command);

        //Assert
        result.Name.Should().Be(command.Name);
        result.CategoryId.Should().Be(1);
        result.FileGuid.Should().Be(command.FileGuid);
        result.HierarchyId.Should().Be(command.HierarchyId);
        result.AncestorName.Should().BeEmpty();
    }
}