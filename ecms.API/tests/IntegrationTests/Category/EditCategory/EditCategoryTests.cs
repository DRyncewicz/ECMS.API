using ecms.Application.Handlers.Commands.Category.EditCategory;
using ecms.Domain.Entities;
using FluentAssertions;
using IntegrationTests.Abstractions;

namespace IntegrationTests.Category.EditCategory;

public class EditCategoryTests : BaseIntegrationTest
{
    public EditCategoryTests(IntegrationTestWebAppFactory factory) : base(factory)
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
    public async Task EditCategoryCommand_ShouldEditCategory_OnValidRequest()
    {
        //Arrange
        var command = new EditCategoryCommand()
        {
            CategoryId = 5,
            Name = "ChangedName",
            AncestorHierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId("/1/"),
        };

        //Act
        var result = await Sender.Send(command);

        //Assert
        var editedCategory = ApplicationDbContext.Categories.FirstOrDefault(p => p.Id == 5);
        editedCategory.Name.Should().Be(command.Name);
        editedCategory.Id.Should().Be(5);
        editedCategory.HierarchyId.IsDescendantOf(command.AncestorHierarchyId);
    }
}