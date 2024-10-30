using AutoMapper;
using ecms.Application.Handlers.Commands.GetOrCreateAddress;
using ecms.Application.Models.Dtos.Addresses;
using ecms.Domain.Entities;
using FluentAssertions;
using UnitTests.Mapping;

namespace UnitTests.MapperProfiles.Addresses;

public class AddressProfileTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;

    public AddressProfileTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
    }

    [Fact]
    public void Should_Map_GetOrCreateAddressCommand_To_AddressEntity()
    {
        // Arrange
        var address = new GetOrCreateAddressCommand
        {
            Country = "Poland",
            City = "Koszalin",
            Street = "Rodła",
            PostalCode = "75-361",
            BuildingNumber = "42",
        };

        // Act
        var result = _mapper.Map<AddressEntity>(address);

        // Assert
        result.Should().NotBeNull();
        result.Country.Should().Be(address.Country);
        result.City.Should().Be(address.City);
        result.Street.Should().Be(address.Street);
        result.PostalCode.Should().Be(address.PostalCode);
        result.BuildingNumber.Should().Be(address.BuildingNumber);
        result.ApartmentNumber.Should().BeNull();
    }

    [Fact]
    public void Should_Map_AddressEntity_To_AddressDto()
    {
        // Arrange
        var address = new AddressEntity
        {
            Id = 1,
            Country = "Poland",
            City = "Koszalin",
            Street = "Rodła",
            PostalCode = "75-361",
            BuildingNumber = "42",
        };

        // Act
        var result = _mapper.Map<AddressDto>(address);

        // Assert
        result.Should().NotBeNull();
        result.AddressId.Should().Be(1);
        result.Country.Should().Be(address.Country);
        result.City.Should().Be(address.City);
        result.Street.Should().Be(address.Street);
        result.PostalCode.Should().Be(address.PostalCode);
        result.BuildingNumber.Should().Be(address.BuildingNumber);
        result.ApartmentNumber.Should().BeNull();
    }
}