using ecms.Application.Handlers.Commands.GetOrCreateAddress;
using ecms.Domain.Entities;
using ecms.Infrastructure.Database;
using FluentAssertions;
using FunctionalTests.Abstractions;
using System.Net;

namespace FunctionalTests.Controllers;

public class AddressControllerTests : BaseFunctionalTest
{
    public AddressControllerTests(FunctionalTestWebAppFactory factory) : base(factory)
    {
        Seed();
    }

    private void Seed()
    {
        var addresses = new List<AddressEntity>
        {
            new AddressEntity
            {
                Country = "Poland",
                City = "Koszalin",
                Street = "Rodła",
                PostalCode = "75-361",
                BuildingNumber = "42",
                ApartmentNumber = ""
            },
            new AddressEntity
            {
                Country = "Country1",
                City = "City1",
                Street = "Street1",
                PostalCode = "432432",
                BuildingNumber = "10",
                ApartmentNumber = "5"
            }
        };

        ApplicationDbContext.Addresses.AddRange(addresses);
        ApplicationDbContext.SaveChanges();
    }

    [Fact]
    public async Task GetOrCreate_ShouldReturnAddressId_OnValidRequest()
    {
        //Arrange
        var command = new GetOrCreateAddressCommand
        {
            Country = "Poland",
            City = "Koszalin",
            Street = "Rodła",
            PostalCode = "75-361",
            BuildingNumber = "42",
            ApartmentNumber = ""
        };

        //Act
        var response = await AuthorizedHttpClient.PostAsJsonAsync("api/v1/Address", command);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}