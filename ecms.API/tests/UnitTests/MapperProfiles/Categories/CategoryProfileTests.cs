using AutoMapper;
using ecms.Application.Handlers.Commands.CreateCategory;
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
}
