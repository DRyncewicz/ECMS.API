using ecms.Application.Handlers.Commands.CreateStock;
using ecms.Application.Handlers.Commands.EditProduct;
using ecms.Application.Handlers.Commands.EditStock;
using ecms.Application.Models.Dtos.Products;
using ecms.Application.Handlers.Commands.DeleteStock;
using ecms.Domain.Entities;
using FluentAssertions;
using FunctionalTests.Abstractions;
using System.Net;

namespace FunctionalTests.Controllers;

public class StockControllerTests : BaseFunctionalTest
{
    public StockControllerTests(FunctionalTestWebAppFactory factory) : base(factory)
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

        ApplicationDbContext.Addresses.Add(address);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.Stocks.Add(stock);
        ApplicationDbContext.SaveChanges();
    }

    [Fact]
    public async Task CreateStock_ShouldCreateStock_OnValidRequest()
    {
        //Arrange
        var command = new CreateStockCommand()
        {
            Name = "Dupa",
            Description = "Dupa",
            AddressId = 1
        };

        //Act
        var response = await AuthorizedHttpClient.PostAsJsonAsync("api/v1/Stock", command);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task EditStock_ShouldEditStock_OnValidRequest()
    {
        //Arrange
        var command = new EditStockRequest()
        {
            Name = "NameTest",
            AddressId = 1,
            Description = "Description",
        };

        //Act
        var response = await AuthorizedHttpClient.PutAsJsonAsync("api/v1/Stock/1", command);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteStock_ShouldDeleteStock_OnValidRequest()
    {
        //Act
        var response = await AuthorizedHttpClient.DeleteAsync("api/v1/Stock/1");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}

