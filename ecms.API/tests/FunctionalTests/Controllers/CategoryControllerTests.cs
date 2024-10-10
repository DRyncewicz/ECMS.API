using ecms.Application.Handlers.Commands.CreateCategory;
using ecms.Domain.Entities;
using FluentAssertions;
using FunctionalTests.Abstractions;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FunctionalTests.Controllers;

public class CategoryControllerTests : BaseFunctionalTest
{
    public CategoryControllerTests(FunctionalTestWebAppFactory factory) : base(factory)
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
    public async Task CreateCategory_ShouldCreateCategory_OnValidRequest()
    {
        //Arrange
        var command = new CreateCategoryCommand()
        {
            AncestorHierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId(),
            Name = "Test",
            FileGuid = Guid.NewGuid(),
        };

        //Act
        var response = await AuthorizedHttpClient.PostAsJsonAsync("api/v1/Category", command);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task GetCategories_ShouldReturnAllCategories_OnValidRequest()
    {
        //Act
        var response = await AuthorizedHttpClient.GetAsync("api/v1/Category");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task DeleteCategory_ShouldDeleteCategory_OnValidRequest()
    {
        //Act
        var response = await AuthorizedHttpClient.DeleteAsync("api/v1/Category/1");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);        
    }
}
