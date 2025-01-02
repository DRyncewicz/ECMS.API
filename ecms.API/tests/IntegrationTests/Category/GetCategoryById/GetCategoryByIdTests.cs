using ecms.Application.Handlers.Queries.Category.GetCategoryById;
using ecms.Application.Models.ViewModels.Categories;
using ecms.Domain.Entities;
using FluentAssertions;
using IntegrationTests.Abstractions;
using SharedKernel;

namespace IntegrationTests.Category.GetCategoryById;

public class GetCategoryByIdTests : BaseIntegrationTest
{
    public GetCategoryByIdTests(IntegrationTestWebAppFactory factory) : base(factory)
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
                Name = "TestCategory2",
            },

            new()
            {
                HierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId("/1/"),
                Name = "TestCategory3",
            },

            new()
            {
                HierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId("/2/"),
                Name = "TestCategory4",
            },

            new()
            {
                HierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId("/1/20/"),
                Name = "TestCategory5",
            }
        };

        ApplicationDbContext.Categories.AddRange(categories);
        ApplicationDbContext.SaveChanges();
    }

    [Fact]
    public async Task GetCategoryById_ShouldReturnSuccessResult_OnValidRequest()
    {
        //Arrange
        var query = new GetCategoryByIdQuery(5);

        //Act
        var result = await Sender.Send(query);

        //Assert
        result.Should().NotBeNull();
        result.Value.CategoryId.Should().Be(5);
        result.Value.Name.Should().Be("TestCategory5");
        result.Value.AncestorName.Should().Be("TestCategory3");
        result.Should().BeOfType<Result<CategoryViewModel>>();
        result.IsSuccess.Should().BeTrue();
    }
}