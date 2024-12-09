using ecms.Application.Handlers.Commands.EditSupplier;
using ecms.Application.Models.Dtos.Suppliers;
using ecms.Domain.Entities;
using FluentAssertions;
using IntegrationTests.Abstractions;

namespace IntegrationTests.EditSupplier;

public class EditSupplierTests : BaseIntegrationTest
{
    public EditSupplierTests(IntegrationTestWebAppFactory factory) : base(factory)
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
            Name = Faker.Commerce.ProductName(),
            AddressId = 1,
        };

        var supplierContact = new SupplierContactEntity
        {
            SupplierId = 1,
            IsActive = true,
            Description = Faker.Commerce.ProductDescription(),
            Email = Faker.Internet.Email(),
            PhoneNumber = Faker.Phone.PhoneNumber(),
            IsCommon = true,
            RepresentativeName = Faker.Commerce.ProductName(),
        };

        ApplicationDbContext.Addresses.Add(address);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.Suppliers.Add(supplier);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.SupplierContacts.Add(supplierContact);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.ChangeTracker.Clear();
    }

    [Fact]
    public async Task EditSupplier_ShouldEditSupplier_OnValidRequest()
    {
        //Arrange
        var supplierContacts = new List<CreateSupplierContactDto>
        {
            new()
            {
                Id = 1,
                SupplierId = 1,
                IsActive = true,
                Description = "DeliveryMan",
                Email = "example@email.com",
                PhoneNumber = "0700",
                IsCommon = true,
                RepresentativeName = "Jakub",
            }
        };

        var command = new EditSupplierCommand()
        {
            SupplierId = 1,
            Name = "Name",
            AddressId = 1,
            Contacts = supplierContacts,
        };

        //Act
        var result = await Sender.Send(command);

        //Assert
        result.Value.Should().Be(1);
        var supplier = ApplicationDbContext.Suppliers.First(p => p.Id == 1);
        supplier.Name.Should().Be("Name");
        supplier.IsActive.Should().Be(true);
        supplier.IsDeleted.Should().Be(false);
        supplier.AddressId.Should().Be(1);
        var supplierContact = ApplicationDbContext.SupplierContacts.First(p => p.Id == 1);
        supplierContact.IsActive.Should().Be(true);
        supplierContact.Description.Should().Be("DeliveryMan");
        supplierContact.Email.Should().Be("example@email.com");
        supplierContact.PhoneNumber.Should().Be("0700");
        supplierContact.RepresentativeName.Should().Be("Jakub");
        supplierContact.IsCommon.Should().Be(true);
    }
}