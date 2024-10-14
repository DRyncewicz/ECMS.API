using ecms.Application.Handlers.Queries.GetAllCategoriesPaged;
using ecms.Application.Models.ViewModels.Categories;
using ecms.Domain.Entities;
using FluentAssertions;
using IntegrationTests.Abstractions;
using SharedKernel;

namespace IntegrationTests.GetAllCategoriesPaged;

public class GetAllCategoriesPagedTests : BaseIntegrationTest
{
    public GetAllCategoriesPagedTests(IntegrationTestWebAppFactory factory) : base(factory)
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
    public async Task GetAllCategoriesPaged_ShouldReturnSuccessResult_OnValidRequest()
    {
        //Arrange
        var query = new GetAllCategoriesPagedQuery();

        //Act
        var result = await Sender.Send(query);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Result<PagedCategoryViewModel>>();
        result.IsSuccess.Should().BeTrue();
        result.Value.Categories.Should().HaveCount(5);
        result.Value.TotalCount.Should().Be(5);
    }
}