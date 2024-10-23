using ecms.Application.Handlers.Commands.CreateStock;
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

        ApplicationDbContext.Addresses.Add(address);
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
}
