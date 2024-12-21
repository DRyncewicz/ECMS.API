using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Queries.GetSupplierDetailsById;
using ecms.Domain.Entities;
using FluentAssertions;
using Moq;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Queries.GetSupplierDetailsById;

public class GetSupplierDetailsByIdQueryHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly GetSupplierDetailsByIdQueryHandler _handler;

    public GetSupplierDetailsByIdQueryHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _handler = new GetSupplierDetailsByIdQueryHandler(_applicationDbContext.Object, _mapper);

        var supplierContacts = new List<SupplierContactEntity>
        {
            new()
            {
                Id = 1,
                SupplierId = 1,
                IsActive = true,
                Description = "ContactDescription",
                Email = "ContactEmail",
                PhoneNumber = "ContactPhoneNumber",
                IsCommon = true,
                RepresentativeName = "ContactName"
            }
        };

        var suppliers = new List<SupplierEntity>
        {
            new()
            {
                Id = 1,
                IsActive = true,
                IsDeleted = false,
                Name = "SupplierName",
                AddressId = 1,
                SupplierContacts = supplierContacts
            },

            new()
            {
                Id = 2,
                IsActive = true,
                IsDeleted = false,
                Name = "SupplierName2",
                AddressId = 1,
            }
        };

        var dbContextResponseSupplierContacts = supplierContacts.AsQueryable().BuildMock();
        _applicationDbContext.Setup(p => p.SupplierContacts).Returns(dbContextResponseSupplierContacts.Object);
        var dbContextResponseSuppliers = suppliers.AsQueryable().BuildMock();
        _applicationDbContext.Setup(p => p.Suppliers).Returns(dbContextResponseSuppliers.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenSupplierWithSupplierContactsExists()
    {
        //Arrange
        var query = new GetSupplierDetailsByIdQuery(1);

        //Act
        var result = await _handler.Handle(query, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Id.Should().Be(1);
        result.Value.Name.Should().Be("SupplierName");
        result.Value.IsActive.Should().Be(true);
        result.Value.AddressId.Should().Be(1);
        result.Value.ContactDtos.First().Id.Should().Be(1);
        result.Value.ContactDtos.First().SupplierId.Should().Be(1);
        result.Value.ContactDtos.First().IsActive.Should().Be(true);
        result.Value.ContactDtos.First().Description.Should().Be("ContactDescription");
        result.Value.ContactDtos.First().Email.Should().Be("ContactEmail");
        result.Value.ContactDtos.First().PhoneNumber.Should().Be("ContactPhoneNumber");
        result.Value.ContactDtos.First().IsCommon.Should().Be(true);
        result.Value.ContactDtos.First().RepresentativeName.Should().Be("ContactName");
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenSupplierWithNoContactsExists()
    {
        //Arrange
        var query = new GetSupplierDetailsByIdQuery(2);

        //Act
        var result = await _handler.Handle(query, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Id.Should().Be(2);
        result.Value.Name.Should().Be("SupplierName2");
        result.Value.IsActive.Should().Be(true);
        result.Value.AddressId.Should().Be(1);
        result.Value.ContactDtos.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_ShouldEarlyReturnResultFailure_WhenMaterialDoesntExist()
    {
        //Arrange
        var query = new GetSupplierDetailsByIdQuery(10);

        //Act
        var result = await _handler.Handle(query, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().NotBeNull();
    }
}