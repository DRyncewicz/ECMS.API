using ecms.Application.Handlers.Queries.Material.GetMaterialDetailsById;
using ecms.Application.Models.ViewModels.Materials;
using ecms.Domain.Entities;
using FluentAssertions;
using IntegrationTests.Abstractions;
using SharedKernel;

namespace IntegrationTests.GetMaterialDetailsById;

public class GetMaterialDetailsByIdTests : BaseIntegrationTest
{
    private readonly Guid fileGuid = Guid.NewGuid();
    private readonly DateTime dateTime = new DateTime(2024, 9, 22);

    public GetMaterialDetailsByIdTests(IntegrationTestWebAppFactory factory) : base(factory)
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
    public async Task GetMaterialDetailsById_ShouldReturnSuccessResult_OnValidRequest()
    {
        //Arrange
        var query = new GetMaterialDetailsByIdQuery(1);

        //Act
        var result = await Sender.Send(query);

        //Assert
        result.Should().NotBeNull();
        result.Value.MaterialId.Should().Be(1);
        result.Value.Name.Should().Be("DodasekGrubasek");
        result.Value.Description.Should().Be("Description");
        result.Value.MaxStockLevel.Should().Be(7);
        result.Value.MinStockLevel.Should().Be(1);
        result.Value.UnitOfMeasure.Should().Be(ecms.Domain.Enums.UnitOfMeasureType.Pieces);
        result.Value.FileGuid.Should().Be(fileGuid);
        result.Value.ReorderLevel.Should().Be(1);
        result.Value.IsActive.Should().Be(true);
        result.Value.StockLevel.BatchNumber.Should().Be("BatchNumber");
        result.Value.StockLevel.LastUpdated.Should().Be(dateTime);
        result.Value.StockLevel.Quantity.Should().Be(1);
        result.Value.StockLevel.StockId.Should().Be(1);
        result.Should().BeOfType<Result<MaterialDetailsViewModel>>();
        result.IsSuccess.Should().BeTrue();
    }
}