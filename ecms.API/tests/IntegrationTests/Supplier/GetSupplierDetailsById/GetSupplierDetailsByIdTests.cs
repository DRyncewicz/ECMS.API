using ecms.Application.Handlers.Queries.Supplier.GetSupplierDetailsById;
using ecms.Application.Models.ViewModels.Suppliers;
using ecms.Domain.Entities;
using FluentAssertions;
using IntegrationTests.Abstractions;
using SharedKernel;

namespace IntegrationTests.Supplier.GetSupplierDetailsById;

public class GetSupplierDetailsByIdTests : BaseIntegrationTest
{
    private string _supplierName;

    public GetSupplierDetailsByIdTests(IntegrationTestWebAppFactory factory) : base(factory)
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

        _supplierName = Faker.Commerce.ProductName();

        var supplier = new SupplierEntity
        {
            IsActive = true,
            IsDeleted = false,
            Name = _supplierName,
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
    public async Task GetSupplierDetailsById_ShouldReturnSuccessResult_OnValidRequest()
    {
        //Arrange
        var query = new GetSupplierDetailsByIdQuery(1);

        //Act
        var result = await Sender.Send(query);

        //Assert
        result.Should().NotBeNull();
        result.Value.IsActive.Should().Be(true);
        result.Value.Name.Should().Be(_supplierName);
        result.Value.AddressId.Should().Be(1);
        result.Value.ContactDtos.First().Id.Should().Be(1);
        result.Value.ContactDtos.First().SupplierId.Should().Be(1);
        result.Value.ContactDtos.First().IsActive.Should().Be(true);
        result.Value.ContactDtos.First().Description.Should().Be("ContactDescription");
        result.Value.ContactDtos.First().Email.Should().Be("ContactEmail");
        result.Value.ContactDtos.First().PhoneNumber.Should().Be("ContactPhoneNumber");
        result.Value.ContactDtos.First().IsCommon.Should().Be(true);
        result.Value.ContactDtos.First().RepresentativeName.Should().Be("ContactName");
        result.Should().BeOfType<Result<SupplierDetailsViewModel>>();
        result.IsSuccess.Should().BeTrue();
    }
}