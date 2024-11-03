using ecms.Application.Handlers.Queries.GetAllStocksWithAddresses;
using ecms.Application.Models.ViewModels.Stocks;
using ecms.Domain.Entities;
using FluentAssertions;
using IntegrationTests.Abstractions;
using SharedKernel;

namespace IntegrationTests.GetAllStocksWithAddresses;

public class GetAllStocksWithAddressesTests : BaseIntegrationTest
{
    public GetAllStocksWithAddressesTests(IntegrationTestWebAppFactory factory) : base(factory)
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
        ApplicationDbContext.Addresses.Add(address);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.Stocks.AddRange(stocks);
        ApplicationDbContext.SaveChanges();
    }

    [Fact]
    public async Task GetAllStocksWithAddresses_ShouldReturnSuccessResult_OnValidRequest()
    {
        //Arrange
        var query = new GetAllStocksWithAddressesQuery();

        //Act
        var result = await Sender.Send(query);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Result<StockViewModel>>();
        result.IsSuccess.Should().BeTrue();
        result.Value.Stocks.Should().HaveCount(2);
    }
}