using ecms.Application.Handlers.Commands.DeleteCategory;
using ecms.Domain.Entities;
using ecms.Infrastructure.Database;
using FluentAssertions;
using IntegrationTests.Abstractions;

namespace IntegrationTests.DeleteCategory;

public class DeleteCategoryTests : BaseIntegrationTest
{
    public DeleteCategoryTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
        Seed();
    }

    private void Seed()
    {
        var categories = new List<CategoryEntity>
        {
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
    public async Task DeleteCategoryCommand_ShouldDeleteCategory_OnValidRequest()
    {
        //Arrange
        var command = new DeleteCategoryCommand(4);

        //Act
        var result = await Sender.Send(command);

        //Assert
        result.Value.Should().Be("");
    }
}