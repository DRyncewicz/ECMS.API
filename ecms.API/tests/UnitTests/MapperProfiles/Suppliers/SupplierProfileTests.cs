using AutoMapper;
using ecms.Application.Handlers.Commands.CreateSupplier;
using ecms.Domain.Entities;
using FluentAssertions;
using UnitTests.Mapping;

namespace UnitTests.MapperProfiles.Suppliers;

public class SupplierProfileTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;

    public SupplierProfileTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
    }

    [Fact]
    public void Should_MapFrom_CreateSupplierCommand_To_MaterialEntity()
    {
        //Arrange
        var command = new CreateSupplierCommand()
        {
            Name = "Name",
            AddressId = 1,
        };

        //Act
        var result = _mapper.Map<SupplierEntity>(command);

        //Assert
        result.Name.Should().Be(command.Name);
        result.AddressId.Should().Be(command.AddressId);
    }

    [Fact]
    public void Should_MapFrom_SupplierEntity_To_SupplierHistoryEntity()
    {
        //Arrange
        var command = new SupplierEntity()
        {
            Id = 1,
            Name = "DodasekGrubasek",
            AddressId = 1,
            IsDeleted = false,
            IsActive = true,
        };

        //Act
        var result = _mapper.Map<SupplierHistoryEntity>(command);

        //Assert
        result.IsActive.Should().Be(command.IsActive);
        result.IsDeleted.Should().Be(command.IsDeleted);
        result.Name.Should().Be(command.Name);
        result.SupplierId.Should().Be(command.Id);
        result.AddressId.Should().Be(command.AddressId);
    }
}
