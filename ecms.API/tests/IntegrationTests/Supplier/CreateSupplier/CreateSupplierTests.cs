using ecms.Application.Handlers.Commands.Supplier.CreateSupplier;
using ecms.Domain.Entities;
using FluentAssertions;
using IntegrationTests.Abstractions;

namespace IntegrationTests.Supplier.CreateSupplier;

public class CreateSupplierTests : BaseIntegrationTest
{
    public CreateSupplierTests(IntegrationTestWebAppFactory factory) : base(factory)
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

        ApplicationDbContext.Addresses.Add(address);
        ApplicationDbContext.SaveChanges();
    }

    [Fact]
    public async Task CreateSupplier_ShouldCreateSupplier_OnValidRequest()
    {
        //Arrange
        var command = new CreateSupplierCommand()
        {
            Name = "Kfd",
            AddressId = 1,
        };

        //Act
        var result = await Sender.Send(command);

        //Assert
        result.Value.Should().Be(1);
    }
}