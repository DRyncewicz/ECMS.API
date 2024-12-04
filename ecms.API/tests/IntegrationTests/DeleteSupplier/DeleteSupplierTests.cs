using ecms.Application.Handlers.Commands.DeleteSupplier;
using ecms.Domain.Entities;
using FluentAssertions;
using IntegrationTests.Abstractions;

namespace IntegrationTests.DeleteSupplier;

public class DeleteSupplierTests : BaseIntegrationTest
{
    public DeleteSupplierTests(IntegrationTestWebAppFactory factory) : base(factory)
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

        var supplier = new SupplierEntity
        {
            IsActive = true,
            IsDeleted = false,
            Name = "KFD",
            AddressId = 1,
        };

        var supplierContact = new SupplierContactEntity
        {
            SupplierId = 1,
            IsActive = true,
            Description = "ContactDescription",
            Email = "ContactEmail",
            PhoneNumber = "ContactPhoneNumber",
            IsCommon = true,
            RepresentativeName = "ContactName"
        };

        ApplicationDbContext.Addresses.Add(address);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.Suppliers.Add(supplier);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.SupplierContacts.Add(supplierContact);
        ApplicationDbContext.SaveChanges();
    }

    [Fact]
    public async Task DeleteSupplierCommand_ShouldDeleteSupplier_OnValidRequest()
    {
        //Arrange
        var command = new DeleteSupplierCommand(1);

        //Act
        var result = await Sender.Send(command);

        //Assert
        result.Value.Should().Be(true);
    }
}