using ecms.Application.Handlers.Commands.CreateCategory;
using ecms.Application.Handlers.Commands.EditProduct;
using ecms.Domain.Entities;
using ecms.Infrastructure.Database;
using FluentAssertions;
using IntegrationTests.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace IntegrationTests.CreateCategory;

public class CreateCategoryTests : BaseIntegrationTest
{
    public CreateCategoryTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
        Seed();
    }

    private void Seed()
    {
        var categories = new List<CategoryEntity>
        {
            new CategoryEntity()
            {
                HierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId(),
                Name = "TestCategory1",
            },

            new()
            {
                HierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId("/1/"),
                Name = "TestCategory2",
            },

            new()
            {
                HierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId("/2/"),
                Name = "TestCategory3",
            },

            new()
            {
                HierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId("/1/20/"),
                Name = "TestCategory4",
            }
        };

        ApplicationDbContext.Categories.AddRange(categories);
        ApplicationDbContext.SaveChanges();
    }

    [Fact]
    public async Task CreateCategoryCommand_ShouldCreateCategory_OnValidRequest()
    {
        //Arrange
        var command = new CreateCategoryCommand()
        {
            AncestorHierarchyId = new HierarchyId(),
            Name = "Test",
            FileGuid = Guid.NewGuid(),
        };

        //Act
        var result = await Sender.Send(command);

        //Assert
        result.Value.Should().BeGreaterThan(0);
        var category = ApplicationDbContext.Categories.FirstOrDefault(p => p.Id == result.Value);
        category.HierarchyId.Should().Be(HierarchyId.Parse("/3/"));
        category.Name.Should().Be("Test");
        category.FileGuid.Should().NotBeNull();

    }
}