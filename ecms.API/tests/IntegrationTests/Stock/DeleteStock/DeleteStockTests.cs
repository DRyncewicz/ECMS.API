using ecms.Application.Handlers.Commands.Stock.DeleteStock;
using ecms.Domain.Entities;
using ecms.Domain.Enums;
using FluentAssertions;
using IntegrationTests.Abstractions;

namespace IntegrationTests.Stock.DeleteStock;

public class DeleteStockTests : BaseIntegrationTest
{
    public DeleteStockTests(IntegrationTestWebAppFactory factory) : base(factory)
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

        var material = new MaterialEntity
        {
            Name = "Material",
            IsActive = true,
            IsDeleted = false,
            UnitOfMeasure = UnitOfMeasureType.Pieces,
            Description = "Description",
            MinStockLevel = 10,
            MaxStockLevel = 100,
            ReorderLevel = 20,
            FileGuid = Guid.NewGuid()
        };

        var stocks = new List<StockEntity>
        {
            new StockEntity
            {
                Description = "Description",
                Name = "Name",
                AddressId = 1,
                IsDeleted = false,
            },

            new()
            {
                Description = "Description2",
                Name = "Name2",
                AddressId = 1,
                IsDeleted = false,
            }
        };

        var stockLevel = new StockLevelEntity
        {
            MaterialId = 1,
            StockId = 1,
            Quantity = 50,
            IsDeleted = false,
            BatchNumber = "Batch1",
            CreateDateTimeUtc = DateTimeOffset.UtcNow
        };

        ApplicationDbContext.Addresses.Add(address);
        ApplicationDbContext.SaveChanges();

        ApplicationDbContext.Materials.Add(material);
        ApplicationDbContext.SaveChanges();

        ApplicationDbContext.Stocks.AddRange(stocks);
        ApplicationDbContext.SaveChanges();

        ApplicationDbContext.StockLevels.Add(stockLevel);
        ApplicationDbContext.SaveChanges();
    }

    [Fact]
    public async Task DeleteStockCommand_ShouldReturnErrorString_OnValidRequest()
    {
        //Arrange
        var command = new DeleteStockCommand(1);

        //Act
        var result = await Sender.Send(command);

        //Assert
        result.Value.Should().Be("Cannot delete stock because there are products in. To delete stock, create internal transfer first");
    }

    [Fact]
    public async Task DeleteStockCommand_ShouldDeleteStock_OnValidRequest()
    {
        //Arrange
        var command = new DeleteStockCommand(2);

        //Act
        var result = await Sender.Send(command);

        //Assert
        result.Value.Should().Be("");
        ApplicationDbContext.Stocks.FirstOrDefault(p => p.Id == 2).IsDeleted.Should().Be(true);
    }
}