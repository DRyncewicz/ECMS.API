using ecms.Application.Handlers.Commands.CreateMaterial;
using ecms.Application.Handlers.Commands.CreateProduct;
using ecms.Application.Handlers.Commands.DeleteMaterial;
using ecms.Application.Models.Dtos.Products;
using ecms.Domain.Entities;
using FluentAssertions;
using FunctionalTests.Abstractions;
using System.Net;

namespace FunctionalTests.Controllers;

public class MaterialControllerTests : BaseFunctionalTest
{
    public MaterialControllerTests(FunctionalTestWebAppFactory factory) : base(factory)
    {
        Seed();
    }

    private void Seed()
    {
        var address = new AddressEntity
        {
            Country = "Dupa",
            Street = "Dupa",
            City = "Dupa",
            PostalCode = "12345",
            BuildingNumber = "12345",
            ApartmentNumber = "12345",
        };

        var stock = new StockEntity
        {
            AddressId = 1,
            Description = "Description",
            Name = "Dupa",
            IsDeleted = false,
        };

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

        var stockLevel = new StockLevelEntity
        {
            BatchNumber = "BatchNumber",
            CreateDateTimeUtc = DateTime.Now,
            IsDeleted = false,
            LastUpdated = DateTime.Now,
            MaterialId = 1,
            Quantity = 1,
            StockId = 1,
        };

        ApplicationDbContext.Addresses.Add(address);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.Stocks.Add(stock);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.Materials.Add(material);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.StockLevels.Add(stockLevel);
        ApplicationDbContext.SaveChanges();
    }

    [Fact]
    public async Task CreateMaterial_ShouldCreateMaterial_OnValidRequest()
    {
        //Arrange
        var command = new CreateMaterialCommand()
        {
            Name = "Name",
            UnitOfMeasure = ecms.Domain.Enums.UnitOfMeasureType.Pieces,
            Description = "Description",
            MinStockLevel = 1,
            MaxStockLevel = 7,
            ReorderLevel = 1,
            FileGuid = Guid.NewGuid(),
            IsActive = true,
            IsDeleted = false,
            StockId = 1,
        };

        //Act
        var response = await AuthorizedHttpClient.PostAsJsonAsync("api/v1/Material", command);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task DeleteMaterial_ShouldDeleteMaterial_OnValidRequest()
    {       
        //Act
        var response = await AuthorizedHttpClient.DeleteAsync("api/v1/Material/1");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetDetailsById_ShouldReturnMaterial_OnValidRequest()
    {
        //Act
        var response = await AuthorizedHttpClient.GetAsync("api/v1/Material/1");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
