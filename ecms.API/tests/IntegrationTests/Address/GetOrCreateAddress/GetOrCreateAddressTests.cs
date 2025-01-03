using ecms.Application.Handlers.Commands.Address.GetOrCreateAddress;
using ecms.Domain.Entities;
using ecms.Infrastructure.Database;
using FluentAssertions;
using IntegrationTests.Abstractions;

namespace IntegrationTests.Address.GetOrCreateAddress;

public class GetOrCreateAddressTests : BaseIntegrationTest
{
    public GetOrCreateAddressTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
        Seed();
    }

    private void Seed()
    {
        var addresses = new List<AddressEntity>
        {
            new AddressEntity
            {
                Country = "Poland",
                City = "Koszalin",
                Street = "Rodła",
                PostalCode = "75-361",
                BuildingNumber = "42",
                ApartmentNumber = ""
            },
            new AddressEntity
            {
                Country = "Country1",
                City = "City1",
                Street = "Street1",
                PostalCode = "432432",
                BuildingNumber = "10",
                ApartmentNumber = "5"
            }
        };

        ApplicationDbContext.Addresses.AddRange(addresses);
        ApplicationDbContext.SaveChanges();
    }

    [Fact]
    public async Task GetOrCreateAddressCommand_ShouldReturnExistingAddressId_OnValidRequest()
    {
        // Arrange
        var command = new GetOrCreateAddressCommand()
        {
            Country = "Poland",
            City = "Koszalin",
            Street = "Rodła",
            PostalCode = "75-361",
            BuildingNumber = "42",
            ApartmentNumber = ""
        };

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.Value.Should().BeGreaterThan(0);
        var existingAddress = ApplicationDbContext.Addresses.FirstOrDefault(p => p.Id == result.Value);
        existingAddress.Should().NotBeNull();
        existingAddress.Country.Should().Be("Poland");
        existingAddress.City.Should().Be("Koszalin");
        existingAddress.Street.Should().Be("Rodła");
        existingAddress.PostalCode.Should().Be("75-361");
        existingAddress.BuildingNumber.Should().Be("42");
        existingAddress.ApartmentNumber.Should().BeEmpty();
    }

    [Fact]
    public async Task GetOrCreateAddressCommand_ShouldCreateNewAddress_OnValidRequest()
    {
        // Arrange
        var command = new GetOrCreateAddressCommand
        {
            Country = "Poland",
            City = "Koszalin",
            Street = "Jana Z Kolna",
            PostalCode = "75-361",
            BuildingNumber = "32",
            ApartmentNumber = "24"
        };

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.Value.Should().BeGreaterThan(0);
        var newAddress = ApplicationDbContext.Addresses.FirstOrDefault(p => p.Id == result.Value);

        newAddress.Should().NotBeNull();
        newAddress.Country.Should().Be("Poland");
        newAddress.City.Should().Be("Koszalin");
        newAddress.Street.Should().Be("Jana Z Kolna");
        newAddress.PostalCode.Should().Be("75-361");
        newAddress.BuildingNumber.Should().Be("32");
        newAddress.ApartmentNumber.Should().Be("24");
    }
}