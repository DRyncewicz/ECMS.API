using Bogus;
using ecms.Application.Handlers.Commands.EditSupplier;
using ecms.Application.Handlers.Commands.Supplier.CreateSupplier;
using ecms.Application.Models.Dtos.SupplierOrders;
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

        var supplierContact = new SupplierContactEntity
        {
            SupplierId = 1,
            IsActive = true,
            Description = Faker.Commerce.ProductDescription(),
            Email = Faker.Internet.Email(),
            PhoneNumber = Faker.Phone.PhoneNumber(),
            IsCommon = true,
            RepresentativeName = Faker.Commerce.ProductName(),
        };

        ApplicationDbContext.Addresses.Add(address);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.Suppliers.Add(supplier);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.SupplierContacts.Add(supplierContact);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.ChangeTracker.Clear();
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

    [Fact]
    public async Task GetDetailsById_Should_ReturnSupplierDetails_OnValidRequest()
    {
        //Act
        var response = await AuthorizedHttpClient.GetAsync("api/v1/Supplier/1");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task DeleteSupplier_ShouldDeleteSupplier_OnValidRequest()
    {
        //Act
        var response = await AuthorizedHttpClient.DeleteAsync("api/v1/Supplier/1");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetSuppliers_ShouldReturnSuppliers_OnValidRequest()
    {
        //Act
        var response = await AuthorizedHttpClient.GetAsync("api/v1/Supplier");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task EditSupplier_ShouldEditSupplier_OnValidRequest()
    {
        //Arrange
        var supplierContacts = new List<CreateSupplierContactDto>
        {
            new()
            {
                Id = 1,
                SupplierId = 1,
                IsActive = true,
                Description = "DeliveryMan",
                Email = "example@email.com",
                PhoneNumber = "0700",
                IsCommon = true,
                RepresentativeName = "Jakub",
            }
        };

        var command = new EditSupplierRequest()
        {
            Name = "Name",
            AddressId = 1,
            SupplierId = 1,
            Contacts = supplierContacts
        };

        //Act
        var response = await AuthorizedHttpClient.PutAsJsonAsync("api/v1/Supplier/1", command);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}