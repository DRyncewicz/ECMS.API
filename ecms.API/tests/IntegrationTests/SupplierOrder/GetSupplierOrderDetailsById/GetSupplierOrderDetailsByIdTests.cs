using ecms.Application.Handlers.Queries.SupplierOrder.GetSupplierOrderDetailsById;
using ecms.Application.Models.ViewModels.SupplierOrders;
using ecms.Domain.Entities;
using ecms.Domain.ValueObjects;
using FluentAssertions;
using IntegrationTests.Abstractions;
using SharedKernel;

namespace IntegrationTests.SupplierOrder.GetSupplierOrderDetailsById;

public class GetSupplierOrderDetailsByIdTests : BaseIntegrationTest
{
    private string _supplierName;
    private string _supplierContactDescription;
    private string _supplierContactEmail;
    private string _supplierContactPhoneNumber;
    private string _supplierContactRepresentativeName;
    private Guid _guid;
    private Price _price;

    public GetSupplierOrderDetailsByIdTests(IntegrationTestWebAppFactory factory) : base(factory)
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
            IsDelivered = true
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
    }

    [Fact]
    public async Task GetSupplierOrderDetailsById_ShouldReturnSuccessResult_OnValidRequest()
    {
        //Arrange
        var query = new GetSupplierOrderDetailsByIdQuery(1);

        //Act
        var result = await Sender.Send(query);

        //Assert
        result.Should().NotBeNull();
        result.Value.SupplierOrderId.Should().Be(1);
        result.Value.DeliveryDate.Should().Be(new DateTime(2025, 9, 22));
        result.Value.SendMessage.Should().Be(false);
        result.Value.CreateDateTimeUtc.Should().Be(new DateTimeOffset(2025, 9, 22, 12, 0, 0, TimeSpan.Zero));
        result.Value.EditDateTimeUtc.Should().Be(new DateTimeOffset(2024, 9, 22, 12, 0, 0, TimeSpan.Zero));
        result.Value.Status.Should().Be(ecms.Domain.Enums.StatusType.Pending);
        result.Value.MessageId.Should().Be(null);
        var firstSupplierOrderMaterialDto = result.Value.SupplierOrderMaterialDtos.First();
        firstSupplierOrderMaterialDto.SupplierOrderMaterialId.Should().Be(1);
        firstSupplierOrderMaterialDto.SupplierOrderId.Should().Be(1);
        firstSupplierOrderMaterialDto.IsDelivered.Should().Be(true);
        firstSupplierOrderMaterialDto.PricePerUnit.Should().Be(_price);
        firstSupplierOrderMaterialDto.Quantity.Should().Be(41);
        firstSupplierOrderMaterialDto.Material.MaterialId.Should().Be(1);
        firstSupplierOrderMaterialDto.Material.Name.Should().Be("DodasekGrubasek");
        firstSupplierOrderMaterialDto.Material.Description.Should().Be("Description");
        firstSupplierOrderMaterialDto.Material.FileGuid.Should().Be(_guid);
        firstSupplierOrderMaterialDto.Material.MinStockLevel.Should().Be(1);
        firstSupplierOrderMaterialDto.Material.MaxStockLevel.Should().Be(7);
        firstSupplierOrderMaterialDto.Material.ReorderLevel.Should().Be(1);
        firstSupplierOrderMaterialDto.Material.StockId.Should().Be(1);
        result.Value.SupplierContactDto.Id.Should().Be(1);
        result.Value.SupplierContactDto.IsActive.Should().Be(true);
        result.Value.SupplierContactDto.IsCommon.Should().Be(true);
        result.Value.SupplierContactDto.SupplierId.Should().Be(1);
        result.Value.SupplierContactDto.RepresentativeName.Should().Be(_supplierContactRepresentativeName);
        result.Value.SupplierContactDto.PhoneNumber.Should().Be(_supplierContactPhoneNumber);
        result.Value.SupplierContactDto.Email.Should().Be(_supplierContactEmail);
        result.Value.SupplierContactDto.Description.Should().Be(_supplierContactDescription);
        result.Value.SupplierDto.Id.Should().Be(1);
        result.Value.SupplierDto.AddressId.Should().Be(1);
        result.Value.SupplierDto.IsActive.Should().Be(true);
        result.Value.SupplierDto.Name.Should().Be(_supplierName);
        result.Should().BeOfType<Result<SupplierOrderDetailsViewModel>>();
        result.IsSuccess.Should().BeTrue();
    }
}