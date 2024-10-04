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
        var category = new CategoryEntity
        {
            HierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId(),
            Name = "TestCategory1",
        };

        ApplicationDbContext.Categories.Add(category);
        ApplicationDbContext.SaveChanges();
    }

    [Fact]
    public async Task CreateCategory_ShouldCreateCategory_OnValidRequest()
    {
        //Arrange
        var command = new CreateCategoryCommand()
        {
            AncenstorHierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId(),
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
}
