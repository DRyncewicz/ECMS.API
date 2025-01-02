using ecms.Application.Models.ViewModels.Suppliers;
using ecms.Domain.Entities;
using FluentAssertions;
using IntegrationTests.Abstractions;
using SharedKernel;
using UnitTests.Handlers.Queries.GetSuppliersPaged;

namespace IntegrationTests.Supplier.GetSuppliersPaged;

public class GetSuppliersPagedTests : BaseIntegrationTest
{
    public GetSuppliersPagedTests(IntegrationTestWebAppFactory factory) : base(factory)
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

        var suppliers = new List<SupplierEntity>
        {
            new()
            {
                IsActive = true,
                IsDeleted = false,
                Name = "KFD",
                AddressId = 1,
            },
            new()
            {
                IsActive = true,
                IsDeleted = false,
                Name = "OLIMP",
                AddressId = 1,
            },
            new()
            {
                IsActive = true,
                IsDeleted = false,
                Name = "SFD",
                AddressId = 1,
            },
            new()
            {
                IsActive = true,
                IsDeleted = false,
                Name = "ACTIVLAB",
                AddressId = 1,
            },
            new()
            {
                IsActive = true,
                IsDeleted = false,
                Name = "TREC",
                AddressId = 1,
            },
            new()
            {
                IsActive = true,
                IsDeleted = false,
                Name = "DZIK",
                AddressId = 1,
            }
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
        ApplicationDbContext.Suppliers.AddRange(suppliers);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.SupplierContacts.Add(supplierContact);
        ApplicationDbContext.SaveChanges();
    }

    [Fact]
    public async Task GetSuppliersPaged_ShouldReturnSuccessResult_OnValidRequest()
    {
        //Arrange
        var query = new GetSuppliersPagedQuery();

        //Act
        var result = await Sender.Send(query);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Result<PagedSupplierViewModel>>();
        result.IsSuccess.Should().BeTrue();
        result.Value.Suppliers.Should().HaveCount(6);
        result.Value.TotalCount.Should().Be(6);
    }
}