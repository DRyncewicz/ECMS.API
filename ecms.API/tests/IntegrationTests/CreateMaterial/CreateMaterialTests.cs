using ecms.Application.Handlers.Commands.CreateMaterial;
using ecms.Domain.Entities;
using IntegrationTests.Abstractions;
using FluentAssertions;

namespace IntegrationTests.CreateMaterial
{
    public class CreateMaterialTests : BaseIntegrationTest
    {
        public CreateMaterialTests(IntegrationTestWebAppFactory factory) : base(factory)
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

            ApplicationDbContext.Addresses.Add(address);
            ApplicationDbContext.SaveChanges();
            ApplicationDbContext.Stocks.Add(stock);
            ApplicationDbContext.SaveChanges();
        }

        [Fact]
        public async Task CreateMaterial_ShouldCreateMaterial_OnValidRequest()
        {
            //Arrange
            var command = new CreateMaterialCommand()
            {
                Name = "Name",
                UnitOfMeasure = ecms.Domain.Enums.UnitOfMeasureType.Pieces,
                Description = "Description",
                MinStockLevel = 1,
                MaxStockLevel = 7,
                ReorderLevel = 1,
                FileGuid = Guid.NewGuid(),
                IsActive = true,
                IsDeleted = false,
                StockId = 1,
            };

            //Act
            var result = await Sender.Send(command);

            //Assert
            result.Value.Should().Be(1);
        }
    }
}

