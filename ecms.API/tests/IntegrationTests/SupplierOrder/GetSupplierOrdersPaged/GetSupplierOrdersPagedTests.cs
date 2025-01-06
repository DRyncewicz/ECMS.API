using ecms.Application.Handlers.Queries.SupplierOrder.GetSupplierOrdersPaged;
using ecms.Application.Models.ViewModels.SupplierOrders;
using ecms.Domain.Entities;
using FluentAssertions;
using IntegrationTests.Abstractions;
using SharedKernel;

namespace IntegrationTests.SupplierOrder.GetSupplierOrdersPaged;

public class GetSupplierOrdersPagedTests : BaseIntegrationTest
{
    public GetSupplierOrdersPagedTests(IntegrationTestWebAppFactory factory) : base(factory)
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
    public async Task GetSupplierOrdersPaged_ShouldReturnSuccessResult_OnValidRequest()
    {
        //Arrange
        var query = new GetSupplierOrdersPagedQuery();

        //Act
        var result = await Sender.Send(query);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Result<SupplierOrdersViewModel>>();
        result.IsSuccess.Should().BeTrue();
        result.Value.SupplierOrders.Should().HaveCount(1);
        result.Value.TotalCount.Should().Be(1);
    }
}