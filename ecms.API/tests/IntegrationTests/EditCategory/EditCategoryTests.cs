using ecms.Application.Handlers.Commands.EditCategory;
using ecms.Domain.Entities;
using FluentAssertions;
using IntegrationTests.Abstractions;

namespace IntegrationTests.EditCategory;

public class EditCategoryTests : BaseIntegrationTest
{
    public EditCategoryTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
        Seed();
    }

    private void Seed()
    {
        var category = new CategoryEntity
        {           
            HierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId("/1/"),
            Name = "TestCategory1",
        };
    }

    [Fact]
    public async Task EditCategoryCommand_ShouldEditCategory_OnValidRequest()
    {
        //Arrange
        var command = new EditCategoryCommand()
        {
            CategoryId = 1,
            Name = "ChangedName",
            AncestorHierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId("/1/1/"),                     
        };

        //Act
        var result = await Sender.Send(command);

        //Assert
        var editedCategory = ApplicationDbContext.Categories.FirstOrDefault(p => p.Id == 1);
        editedCategory.Name.Should().Be(command.Name);
        editedCategory.Id.Should().Be(1);
        editedCategory.HierarchyId.IsDescendantOf(command.AncestorHierarchyId);
    }
}


        
