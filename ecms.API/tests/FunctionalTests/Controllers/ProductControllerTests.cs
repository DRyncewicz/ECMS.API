using ecms.Application.Handlers.Commands.CreateProduct;
using ecms.Application.Models.Dtos.Products;
using ecms.Application.Models.ViewModels.Products;
using ecms.Domain.Entities;
using FluentAssertions;
using FunctionalTests.Abstractions;
using SharedKernel;
using System.Net;

namespace FunctionalTests.Controllers;

public class ProductControllerTests : BaseFunctionalTest
{
    public ProductControllerTests(FunctionalTestWebAppFactory factory) : base(factory)
    {
        Seed();
    }

    [Fact]
    public async Task GetProducts_ShouldReturnProducts_OnValidRequest()
    {
        //Act
        var response = await AuthorizedHttpClient.GetAsync("api/v1/Product/by-filters");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task DeleteProduct_ShouldDeleteProduct_OnValidRequest()
    {
        //Act
        var response = await AuthorizedHttpClient.DeleteAsync("api/v1/Product/1");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);       
    }

    [Fact]
    public async Task CreateProduct_ShouldCreateProduct_OnValidRequest()
    {
        //Arrange
        var command = new CreateProductCommand()
        {
            Name = "name",
            Description = "description",
            CategoryId = 1,
            AlcoholContent = ecms.Domain.Enums.AlcoholContentType.AlcoholFree,
            Vat = 23,
            Unit = ecms.Domain.Enums.UnitType.Piece,
            ProductVariants = new List<CreateProductVariantDto>()
            {
                new CreateProductVariantDto()
                {
                    Name = "name",
                    Price = new ecms.Domain.ValueObjects.Price(32, ecms.Domain.ValueObjects.Currency.Pln)
                }
            }
        };

        //Act
        var response = await AuthorizedHttpClient.PostAsJsonAsync("api/v1/Product", command);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
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
}
