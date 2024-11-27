using Bogus;
using ecms.Application.Handlers.Commands.CreateSupplier;
using ecms.Domain.Entities;
using FluentAssertions;
using FunctionalTests.Abstractions;
using System.Net;

namespace FunctionalTests.Controllers;

public class SupplierControllerTests : BaseFunctionalTest
{
    public SupplierControllerTests(FunctionalTestWebAppFactory factory) : base(factory)
    {
        Seed();
    }

    private void Seed()
    {
        var address = new AddressEntity
        {
            Country = Faker.Address.Country(),
            City = Faker.Address.City(),
            Street = Faker.Address.StreetName(),
            PostalCode = Faker.Address.ZipCode(),
            BuildingNumber = Faker.Address.BuildingNumber(),
            ApartmentNumber = Faker.Address.BuildingNumber(),
        };

        var supplier = new SupplierEntity
        {
            IsActive = true,
            IsDeleted = false,
            Name = Faker.Commerce.ProductName(),
            AddressId = 1,
        };

        ApplicationDbContext.Addresses.Add(address);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.Suppliers.Add(supplier);
        ApplicationDbContext.SaveChanges();
    }

    [Fact]
    public async Task CreateSupplier_ShouldCreateSupplier_OnValidRequest()
    {
        //Arrange
        var command = new CreateSupplierCommand()
        {
            Name = "Kfd",
            AddressId = 1,
        };

        //Act
        var response = await AuthorizedHttpClient.PostAsJsonAsync("api/v1/Supplier", command);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}
