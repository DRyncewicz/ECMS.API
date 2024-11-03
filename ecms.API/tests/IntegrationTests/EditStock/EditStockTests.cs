using ecms.Application.Handlers.Commands.EditStock;
using ecms.Domain.Entities;
using ecms.Infrastructure.Database;
using FluentAssertions;
using IntegrationTests.Abstractions;

namespace IntegrationTests.EditStock;

public class EditStockTests : BaseIntegrationTest
{
    public EditStockTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
        Seed();
    }

    private void Seed()
    {
        var address = new AddressEntity()
        {
            Country = "Dupa",
            City = "Dupa",
            Street = "Dupa",
            PostalCode = "Dupa",
            BuildingNumber = "Dupa",
            ApartmentNumber = "Dupa",
        };

        var stocks = new List<StockEntity>
        {
            new StockEntity()
            {
                Name = "Dupa",
                AddressId = 1,
                Description = "Dupa",
                IsDeleted = false,
            },
            new StockEntity()
            {
                Name = "Dupa2",
                AddressId = 1,
                Description = "Dupa2",
                IsDeleted = false,
            },
            new StockEntity()
            {
                Name = "Dupa3",
                AddressId = 1,
                Description = "Dupa3",
                IsDeleted = false,
            }
        };

        ApplicationDbContext.Addresses.Add(address);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.Stocks.AddRange(stocks);
        ApplicationDbContext.SaveChanges();
    }

    [Fact]
    public async Task EditStockCommand_ShouldEditStock_OnValidRequest()
    {
        //Arrange
        var command = new EditStockCommand()
        {
            StockId = 1,
            Name = "EditedDupa",
            AddressId = 1,
            Description = "EditedDupa"
        };
        ApplicationDbContext.ChangeTracker.Clear();

        //Act
        var result = await Sender.Send(command);

        //Assert
        var editedStock = ApplicationDbContext.Stocks.FirstOrDefault(p => p.Id == 1);
        editedStock.Name.Should().Be(command.Name);
        editedStock.Id.Should().Be(1);
        editedStock.AddressId.Should().Be(command.AddressId);
        editedStock.Description.Should().Be(command.Description);
    }
}