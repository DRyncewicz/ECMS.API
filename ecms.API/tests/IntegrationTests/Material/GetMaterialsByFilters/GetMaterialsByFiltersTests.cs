using ecms.Application.Handlers.Queries.Material.GetMaterialsByFilters;
using ecms.Application.Models.ViewModels.Materials;
using ecms.Domain.Entities;
using FluentAssertions;
using IntegrationTests.Abstractions;
using SharedKernel;

namespace IntegrationTests.Material.GetMaterialsByFilters;

public class GetMaterialsByFiltersTests : BaseIntegrationTest
{
    private readonly Guid fileGuid = Guid.NewGuid();
    private readonly DateTime dateTime = new DateTime(2024, 9, 22);

    public GetMaterialsByFiltersTests(IntegrationTestWebAppFactory factory) : base(factory)
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

        var stock = new StockEntity
        {
            AddressId = 1,
            Description = "Description",
            Name = "Dupa",
            IsDeleted = false,
        };

        var material = new MaterialEntity
        {
            Name = "DodasekGrubasek",
            MaxStockLevel = 7,
            MinStockLevel = 1,
            UnitOfMeasure = ecms.Domain.Enums.UnitOfMeasureType.Pieces,
            FileGuid = fileGuid,
            Description = "Description",
            ReorderLevel = 1,
            IsDeleted = false,
            IsActive = true,
        };

        var stockLevel = new StockLevelEntity
        {
            BatchNumber = "BatchNumber",
            CreateDateTimeUtc = dateTime,
            IsDeleted = false,
            LastUpdated = dateTime,
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
    public async Task GetMaterialsByFiltersQuery_ShouldReturnSuccessResult_OnValidRequest()
    {
        //Arrange
        var query = new GetMaterialsByFiltersQuery();

        //Act
        var result = await Sender.Send(query);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Result<FilteredMaterialsViewModel>>();
        result.IsSuccess.Should().BeTrue();
        result.Value.Materials.Should().HaveCount(1);
        result.Value.TotalCount.Should().Be(1);
    }
}