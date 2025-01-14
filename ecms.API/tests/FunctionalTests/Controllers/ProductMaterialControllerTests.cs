using ecms.Application.Handlers.Queries.Product.GetGroupProductMaterialsByFilters;
using ecms.Domain.Entities;
using ecms.Domain.ValueObjects;
using FluentAssertions;
using FunctionalTests.Abstractions;
using System.Net;

namespace FunctionalTests.Controllers;

public class ProductMaterialControllerTests : BaseFunctionalTest
{
    public ProductMaterialControllerTests(FunctionalTestWebAppFactory factory) : base(factory)
    {
        Seed();
    }

    private void Seed()
    {
        var material = new MaterialEntity
        {
            Name = "DodasekGrubasek",
            MaxStockLevel = 7,
            MinStockLevel = 1,
            UnitOfMeasure = ecms.Domain.Enums.UnitOfMeasureType.Pieces,
            FileGuid = Guid.NewGuid(),
            Description = "Description",
            ReorderLevel = 1,
            IsDeleted = false,
            IsActive = true,
        };

        var category = new CategoryEntity
        {
            HierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId(),
            Name = "TestCategory1",
        };

        var product = new ProductEntity
        {
            Name = "Test1",
            Description = "Test Description",
            AlcoholContent = ecms.Domain.Enums.AlcoholContentType.UpTo4AndAHalfPercentOrBeer,
            Vat = 23,
            Unit = ecms.Domain.Enums.UnitType.Weight,
            CategoryId = 1
        };

        var productVariant = new ProductVariantEntity
        {
            Name = "Variant1",
            Price = new Price(10, Currency.Pln),
            ProductId = 1,
            IsDeleted = false,
        };

        var productMaterial = new ProductMaterialEntity
        {
            ProductVariantId = 1,
            MaterialId = 1,
            IsDeleted = false,
            Quantity = 1,
        };

        ApplicationDbContext.Categories.Add(category);
        ApplicationDbContext.SaveChanges();

        ApplicationDbContext.Products.AddRange(product);
        ApplicationDbContext.SaveChanges();

        ApplicationDbContext.Materials.Add(material);
        ApplicationDbContext.SaveChanges();

        ApplicationDbContext.ProductVariants.Add(productVariant);
        ApplicationDbContext.SaveChanges();

        ApplicationDbContext.ProductMaterials.Add(productMaterial);
        ApplicationDbContext.SaveChanges();
    }

    [Fact]
    public async Task GetGroupProductMaterialsAsync_ShouldReturnGroupedProductMaterials_OnValidRequest()
    {
        // Arrange
        var query = new GetGroupProductMaterialsByFiltersQuery
        {
            ProductVariantIds = new List<int> { 1 },
            MaterialIds = new List<int> { 1 }
        };

        // Act
        var response = await AuthorizedHttpClient.GetAsync("api/v1/ProductMaterial?ProductVariantIds=1&MaterialIds=1");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}