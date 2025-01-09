using ecms.Application.Handlers.Commands.SupplierOrder.EditSupplierOrder;
using ecms.Application.Models.Dtos.SupplierOrders;
using ecms.Domain.Entities;
using ecms.Domain.ValueObjects;
using FluentAssertions;
using IntegrationTests.Abstractions;

namespace IntegrationTests.SupplierOrder.EditSupplierOrder;

public class EditSupplierOrderTests : BaseIntegrationTest
{
    private string _supplierName;
    private string _supplierContactDescription;
    private string _supplierContactEmail;
    private string _supplierContactPhoneNumber;
    private string _supplierContactRepresentativeName;
    private Guid _guid;
    private Price _price;

    public EditSupplierOrderTests(IntegrationTestWebAppFactory factory) : base(factory)
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

        var stock = new StockEntity
        {
            AddressId = 1,
            Description = "Description",
            Name = "Dupa",
            IsDeleted = false,
        };

        _guid = Guid.NewGuid();
        var material = new MaterialEntity
        {
            Name = "DodasekGrubasek",
            MaxStockLevel = 7,
            MinStockLevel = 1,
            UnitOfMeasure = ecms.Domain.Enums.UnitOfMeasureType.Pieces,
            FileGuid = _guid,
            Description = "Description",
            ReorderLevel = 1,
            IsDeleted = false,
            IsActive = true,
        };

        var stockLevel = new StockLevelEntity
        {
            BatchNumber = "BatchNumber",
            CreateDateTimeUtc = new DateTime(2024, 9, 22),
            IsDeleted = false,
            LastUpdated = new DateTime(2024, 12, 22),
            MaterialId = 1,
            Quantity = 1,
            StockId = 1,
        };

        _supplierName = Faker.Commerce.ProductName();
        var supplier = new SupplierEntity
        {
            IsActive = true,
            IsDeleted = false,
            Name = _supplierName,
            AddressId = 1,
        };

        _supplierContactDescription = Faker.Commerce.ProductDescription();
        _supplierContactEmail = Faker.Internet.Email();
        _supplierContactPhoneNumber = Faker.Phone.PhoneNumber();
        _supplierContactRepresentativeName = Faker.Person.FullName;
        var supplierContact = new SupplierContactEntity
        {
            SupplierId = 1,
            IsActive = true,
            Description = _supplierContactDescription,
            Email = _supplierContactEmail,
            PhoneNumber = _supplierContactPhoneNumber,
            IsCommon = true,
            RepresentativeName = _supplierContactRepresentativeName
        };

        var supplierOrder = new SupplierOrderEntity()
        {
            SupplierId = 1,
            SupplierContactId = 1,
            DeliveryDate = new DateTime(2025, 9, 22),
            CreateDateTimeUtc = new DateTimeOffset(2025, 9, 22, 12, 0, 0, TimeSpan.Zero),
            EditDateTimeUtc = new DateTimeOffset(2024, 9, 22, 12, 0, 0, 0, TimeSpan.Zero),
            Status = ecms.Domain.Enums.StatusType.Pending
        };

        _price = new Price(25, Currency.Usd);
        var supplierOrderMaterial = new SupplierOrderMaterialEntity()
        {
            SupplierOrderId = 1,
            MaterialId = 1,
            Quantity = 41,
            PricePerUnit = _price,
        };

        ApplicationDbContext.Addresses.Add(address);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.Stocks.Add(stock);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.Materials.Add(material);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.StockLevels.Add(stockLevel);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.Suppliers.Add(supplier);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.SupplierContacts.Add(supplierContact);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.SupplierOrders.Add(supplierOrder);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.SupplierOrderMaterials.Add(supplierOrderMaterial);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.ChangeTracker.Clear();
    }

    [Fact]
    public async Task EditSupplierOrder_ShouldEditSupplierOrder_OnValidRequest()
    {
        // Arrange
        var supplierOrderMaterials = new List<EditSupplierOrderMaterialDto>
        {
            new()
            {
                SupplierOrderMaterialId = 1,
                MaterialId = 1,
                Quantity = 50,
                PricePerUnit = new Price(30, Currency.Usd),
                Discount = 5,
            }
        };

        var command = new EditSupplierOrderCommand()
        {
            SupplierOrderId = 1,
            SupplierId = 1,
            SupplierContactId = 1,
            DeliveryDate = new DateTime(2025, 10, 22),
            EditSupplierOrderMaterialDtos = supplierOrderMaterials,
            SendMessage = true,
            Language = ecms.Domain.Enums.LanguageType.English
        };

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.Value.Should().Be(1);
        var supplierOrder = ApplicationDbContext.SupplierOrders.First(p => p.Id == 1);
        supplierOrder.SupplierContactId.Should().Be(1);
        supplierOrder.SupplierId.Should().Be(1);
        supplierOrder.MessageId.Should().NotBeNull();
        supplierOrder.DeliveryDate.Should().Be(new DateTime(2025, 10, 22));
        var supplierOrderMaterial = ApplicationDbContext.SupplierOrderMaterials.First(p => p.SupplierOrderId == 1);
        supplierOrderMaterial.Quantity.Should().Be(50);
        supplierOrderMaterial.MaterialId.Should().Be(1);
        supplierOrderMaterial.PricePerUnit.Amount.Should().Be(30);
        supplierOrderMaterial.PricePerUnit.Currency.Should().Be(Currency.Usd);
    }
}