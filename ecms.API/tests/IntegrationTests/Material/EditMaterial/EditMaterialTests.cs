using Bogus;
using ecms.Application.Handlers.Commands.Material.EditMaterial;
using ecms.Domain.Entities;
using FluentAssertions;
using IntegrationTests.Abstractions;

namespace IntegrationTests.Material.EditMaterial;

public class EditMaterialTests : BaseIntegrationTest

{
    private readonly Guid fileGuid = Guid.NewGuid();

    public EditMaterialTests(IntegrationTestWebAppFactory factory) : base(factory)
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
            Description = Faker.Commerce.ProductDescription(),
            Name = Faker.Commerce.ProductName(),
            IsDeleted = false,
        };

        var material = new MaterialEntity
        {
            Name = Faker.Name.FindName(),
            MaxStockLevel = 7,
            MinStockLevel = 1,
            UnitOfMeasure = ecms.Domain.Enums.UnitOfMeasureType.Pieces,
            FileGuid = fileGuid,
            Description = Faker.Commerce.ProductDescription(),
            ReorderLevel = 3,
            IsDeleted = false,
            IsActive = true,
        };

        var stockLevel = new StockLevelEntity
        {
            BatchNumber = Faker.Commerce.ProductAdjective(),
            CreateDateTimeUtc = new DateTime(2024, 11, 26),
            IsDeleted = false,
            LastUpdated = new DateTime(2024, 11, 26),
            MaterialId = 1,
            Quantity = 1,
            StockId = 1,
        };

        ApplicationDbContext.Addresses.Add(address);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.Stocks.Add(stock);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.Materials.Add(material);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.StockLevels.Add(stockLevel);
        ApplicationDbContext.SaveChanges();
    }

    [Fact]
    public async Task EditMaterial_ShouldEditMaterial_OnValidRequest()
    {
        //Arrange
        var command = new EditMaterialCommand()
        {
            MaterialId = 1,
            Name = Faker.Name.FindName(),
            MaxStockLevel = 7,
            MinStockLevel = 1,
            UnitOfMeasure = ecms.Domain.Enums.UnitOfMeasureType.Pieces,
            FileGuid = fileGuid,
            Description = Faker.Commerce.ProductDescription(),
            ReorderLevel = 3,
            IsActive = true,
            BatchNumber = "BatchNumber",
            StockId = 1,
        };

        //Act
        var result = await Sender.Send(command);

        //Assert
        result.Value.Should().Be(1);
    }
}