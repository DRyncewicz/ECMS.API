using ecms.Application.Handlers.Commands.SupplierOrder.CreateSupplierOrder;
using ecms.Application.Handlers.Commands.SupplierOrder.EditSupplierOrder;
using ecms.Application.Models.Dtos.SupplierOrders;
using ecms.Domain.Entities;
using ecms.Domain.ValueObjects;
using FluentAssertions;
using FunctionalTests.Abstractions;
using System.Net;

namespace FunctionalTests.Controllers;

public class SupplierOrderControllerTests : BaseFunctionalTest
{
    public SupplierOrderControllerTests(FunctionalTestWebAppFactory factory) : base(factory)
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

        var supplierOrder = new SupplierOrderEntity()
        {
            SupplierId = 1,
            SupplierContactId = 1,
            DeliveryDate = new DateTime(2025, 9, 22),
            CreateDateTimeUtc = new DateTimeOffset(2025, 9, 22, 12, 0, 0, TimeSpan.Zero),
            EditDateTimeUtc = new DateTimeOffset(2024, 9, 22, 12, 0, 0, 0, TimeSpan.Zero),
            Status = ecms.Domain.Enums.StatusType.Pending,
        };

        var supplierOrderMaterial = new SupplierOrderMaterialEntity()
        {
            SupplierOrderId = 1,
            MaterialId = 1,
            Quantity = 41,
            PricePerUnit = new ecms.Domain.ValueObjects.Price(25, ecms.Domain.ValueObjects.Currency.Usd),
        };

        ApplicationDbContext.Materials.Add(material);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.Addresses.Add(address);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.Suppliers.Add(supplier);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.SupplierContacts.Add(supplierContact);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.SupplierOrders.Add(supplierOrder);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.SupplierOrderMaterials.Add(supplierOrderMaterial);
        ApplicationDbContext.SaveChanges();
    }

    [Fact]
    public async Task CreateSupplierOrder_ShouldCreateSupplierOrder_OnValidRequest()
    {
        //Arrange
        var supplierOrderMaterialDtos = new List<CreateSupplierOrderMaterialDto>
        {
            new()
            {
                MaterialId = 1,
                Discount = 23,
                PricePerUnit = new ecms.Domain.ValueObjects.Price(25, ecms.Domain.ValueObjects.Currency.Usd),
                Quantity = 17,
            }
        };

        var command = new CreateSupplierOrderCommand()
        {
            SupplierId = 1,
            DeliveryDate = new DateTime(2025, 09, 22),
            Language = ecms.Domain.Enums.LanguageType.English,
            SendMessage = true,
            SupplierContactId = 1,
            SupplierOrderMaterialDtos = supplierOrderMaterialDtos
        };

        //Act
        var response = await AuthorizedHttpClient.PostAsJsonAsync("api/v1/SupplierOrder", command);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task GetSupplierOrders_ShouldReturnSupplierOrders_OnValidRequest()
    {
        //Act
        var response = await AuthorizedHttpClient.GetAsync("api/v1/SupplierOrder");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetDetailsById_Should_ReturnSupplierOrderDetails_OnValidRequest()
    {
        //Act
        var response = await AuthorizedHttpClient.GetAsync("api/v1/SupplierOrder/1");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task EditSupplierOrder_ShouldEditSupplierOrder_OnValidRequest()
    {
        // Arrange
        var editSupplierOrderMaterialDtos = new List<EditSupplierOrderMaterialDto>
        {
            new()
            {
                SupplierOrderMaterialId = 1,
                MaterialId = 1,
                Quantity = 20,
                PricePerUnit = new Price(30, Currency.Usd),
                Discount = 10,
            }
        };

        var request = new EditSupplierOrderRequest()
        {
            SupplierOrderId = 1,
            SupplierId = 1,
            SupplierContactId = 1,
            Language = ecms.Domain.Enums.LanguageType.English,
            DeliveryDate = new DateTime(2025, 09, 22),
            SendMessage = true,
            EditSupplierOrderMaterialDtos = editSupplierOrderMaterialDtos
        };

        // Act
        var response = await AuthorizedHttpClient.PutAsJsonAsync("api/v1/SupplierOrder/1", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}