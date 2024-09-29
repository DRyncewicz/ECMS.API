using ecms.Application.Handlers.Queries.GetProductsByFilters;
using ecms.Application.Models.ViewModels.Products;
using ecms.Domain.Entities;
using FluentAssertions;
using IntegrationTests.Abstractions;
using SharedKernel;

namespace IntegrationTests.GetProductByFilters;

public class GetProductByFiltersTests : BaseIntegrationTest
{
    public GetProductByFiltersTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
        Seed();
    }

    private void Seed()
    {
        var products = new List<ProductEntity>
        {
             new ProductEntity {Name = "Test1", Description = "Test Description", AlcoholContent = ecms.Domain.Enums.AlcoholContentType.UpTo4AndAHalfPercentOrBeer, Vat = 23, Unit = ecms.Domain.Enums.UnitType.Weight, UserId = "userId", CategoryId = 1 },
             new ProductEntity {Name = "Test2", Description = "Test Description", AlcoholContent = ecms.Domain.Enums.AlcoholContentType.UpTo4AndAHalfPercentOrBeer, Vat = 23, Unit = ecms.Domain.Enums.UnitType.Weight, UserId = "userId", CategoryId = 1 },
             new ProductEntity {Name = "Test3", Description = "Test Description", AlcoholContent = ecms.Domain.Enums.AlcoholContentType.UpTo4AndAHalfPercentOrBeer, Vat = 23, Unit = ecms.Domain.Enums.UnitType.Weight, UserId = "userId", CategoryId = 1 }
        };

        var category = new CategoryEntity
        {
            HierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId(),
            Name = "TestCategory1",
        };

        ApplicationDbContext.Categories.Add(category);
        ApplicationDbContext.SaveChanges();

        ApplicationDbContext.Products.AddRange(products);
        ApplicationDbContext.SaveChanges();
    }

    [Fact]
    public async Task GetProductsByFiltersQuery_ShouldReturnSuccessResult_OnValidRequest()
    {
        //Arrange
        var query = new GetProductsByFiltersQuery();

        //Act
        var result = await Sender.Send(query);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Result<FilteredProductsViewModel>>();
        result.IsSuccess.Should().BeTrue();
        result.Value.Products.Should().HaveCount(3);
        result.Value.TotalCount.Should().Be(3);
    }
}
