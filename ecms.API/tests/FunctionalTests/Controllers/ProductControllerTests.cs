using ecms.Application.Handlers.Commands.CreateProduct;
using ecms.Application.Handlers.Commands.EditProduct;
using ecms.Application.Models.Dtos.Products;
using ecms.Domain.Entities;
using FluentAssertions;
using FunctionalTests.Abstractions;
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

    [Fact]
    public async Task EditProduct_ShouldEditProduct_OnValidRequest()
    {
        //Arrange
        var command = new EditProductRequest()
        {
            Name = "NameTest",
            CategoryId = 1,
            AlcoholContent = ecms.Domain.Enums.AlcoholContentType.Between4AndAHalfAnd18ExceptBeer,
            Description = "Description",
            Unit = ecms.Domain.Enums.UnitType.Portion,
            Vat = 8,
            ProductVariants = new List<ProductVariantDto>()
            {
                new ProductVariantDto()
                {
                    Id = 1,
                    ProductId = 1,
                    Name = "Nejm",
                    Price = new ecms.Domain.ValueObjects.Price(22, ecms.Domain.ValueObjects.Currency.Pln),
                },
                new()
                {
                    Id = 0,
                    ProductId = 1,
                    Name = "Test",
                    Price = new ecms.Domain.ValueObjects.Price(22, ecms.Domain.ValueObjects.Currency.Usd)
                }
            }
        };

        //Act
        var response = await AuthorizedHttpClient.PutAsJsonAsync("api/v1/Product/1", command);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task GetDetailsById_ShouldReturnProduct_OnValidRequest()
    {
        //Act
        var response = await AuthorizedHttpClient.GetAsync("api/v1/Product/3");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    private void Seed()
    {
        var products = new List<ProductEntity>
        {
             new ProductEntity
             {
                 Name = "Test1",
                 Description = "Test Description",
                 AlcoholContent = ecms.Domain.Enums.AlcoholContentType.UpTo4AndAHalfPercentOrBeer,
                 Vat = 23,
                 Unit = ecms.Domain.Enums.UnitType.Weight,
                 CategoryId = 1
             },
             new ProductEntity
             {
                 Name = "Test2",
                 Description = "Test Description",
                 AlcoholContent = ecms.Domain.Enums.AlcoholContentType.UpTo4AndAHalfPercentOrBeer,
                 Vat = 23,
                 Unit = ecms.Domain.Enums.UnitType.Weight,
                 CategoryId = 1
             },
             new ProductEntity
             {
                 Name = "Test3",
                 Description = "Test Description",
                 AlcoholContent = ecms.Domain.Enums.AlcoholContentType.UpTo4AndAHalfPercentOrBeer,
                 Vat = 23,
                 Unit = ecms.Domain.Enums.UnitType.Weight,
                 CategoryId = 1
             }
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