using ecms.Application.Handlers.Commands.SupplierOrder.CreateSupplierOrder;
using ecms.Application.Models.Dtos.SupplierOrders;
using ecms.Domain.Entities;
using FluentAssertions;
using IntegrationTests.Abstractions;

namespace IntegrationTests.SupplierOrder.CreateSupplierOrder;

public class CreateSupplierOrderTests : BaseIntegrationTest
{
    public CreateSupplierOrderTests(IntegrationTestWebAppFactory factory) : base(factory)
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

        ApplicationDbContext.Materials.Add(material);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.Addresses.Add(address);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.Suppliers.Add(supplier);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.SupplierContacts.Add(supplierContact);
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
        var result = await Sender.Send(command);

        //Assert
        result.Value.Should().Be(1);
    }
}