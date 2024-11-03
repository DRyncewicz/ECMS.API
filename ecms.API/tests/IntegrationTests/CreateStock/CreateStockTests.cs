using ecms.Application.Handlers.Commands.CreateStock;
using ecms.Domain.Entities;
using FluentAssertions;
using IntegrationTests.Abstractions;

namespace IntegrationTests.CreateStock
{
    public class CreateStockTests : BaseIntegrationTest
    {
        public CreateStockTests(IntegrationTestWebAppFactory factory) : base(factory)
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

            ApplicationDbContext.Addresses.Add(address);
            ApplicationDbContext.SaveChanges();
        }

        [Fact]
        public async Task CreateStockCommand_ShouldCreateStock_OnValidRequest()
        {
            // Arrange
            var command = new CreateStockCommand()
            {
                Name = "Dupa",
                AddressId = 1,
                Description = "Dupa"
            };

            // Act
            var result = await Sender.Send(command);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeGreaterThan(0);

            var stockEntity = await ApplicationDbContext.Stocks.FindAsync(result.Value);
            stockEntity.Should().NotBeNull();
            stockEntity.Name.Should().Be(command.Name);
            stockEntity.AddressId.Should().Be(command.AddressId);
            stockEntity.Description.Should().Be(command.Description);
            stockEntity.IsDeleted.Should().BeFalse();
        }
    }
}