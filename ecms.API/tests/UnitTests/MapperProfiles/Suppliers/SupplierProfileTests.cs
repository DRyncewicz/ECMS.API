using AutoMapper;
using ecms.Application.Handlers.Commands.EditSupplier;
using ecms.Application.Handlers.Commands.Supplier.CreateSupplier;
using ecms.Application.Models.Dtos.SupplierOrders;
using ecms.Application.Models.Dtos.Suppliers;
using ecms.Application.Models.ViewModels.Suppliers;
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
    public void Should_MapFrom_CreateSupplierCommand_To_SupplierEntity()
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

    [Fact]
    public void Should_MapFrom_SupplierEntity_To_SupplierDetailsViewModel()
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
        var result = _mapper.Map<SupplierDetailsViewModel>(command);

        //Assert
        result.IsActive.Should().Be(command.IsActive);
        result.Id.Should().Be(command.Id);
        result.Name.Should().Be(command.Name);
        result.ContactDtos.Should().BeEmpty();
        result.AddressId.Should().Be(command.AddressId);
    }

    [Fact]
    public void Should_MapFrom_SupplierContactEntity_To_SupplierContactDto()
    {
        //Arrange
        var command = new SupplierContactEntity()
        {
            Id = 1,
            RepresentativeName = "Patrykos",
            IsCommon = true,
            SupplierId = 1,
            PhoneNumber = "6954934543",
            Email = "email@email.pl",
            Description = "Description",
            IsActive = true,
        };

        //Act
        var result = _mapper.Map<SupplierContactDto>(command);

        //Assert
        result.Id.Should().Be(command.Id);
        result.IsActive.Should().Be(command.IsActive);
        result.IsCommon.Should().Be(command.IsCommon);
        result.RepresentativeName.Should().Be(command.RepresentativeName);
        result.SupplierId.Should().Be(command.SupplierId);
        result.PhoneNumber.Should().Be(command.PhoneNumber);
        result.Description.Should().Be(command.Description);
        result.Email.Should().Be(command.Email);
    }

    [Fact]
    public void Should_MapFrom_SupplierEntity_To_SupplierDto()
    {
        //Arrange
        var command = new SupplierEntity()
        {
            Id = 1,
            Name = "DodasekGrubasek",
            AddressId = 1,
            IsActive = true,
        };

        //Act
        var result = _mapper.Map<SupplierDto>(command);

        //Assert
        result.IsActive.Should().Be(command.IsActive);
        result.Id.Should().Be(command.Id);
        result.Name.Should().Be(command.Name);
        result.AddressId.Should().Be(command.AddressId);
    }

    [Fact]
    public void Should_MapFrom_EditSupplierCommand_To_SupplierEntity()
    {
        //Arrange
        var command = new EditSupplierCommand()
        {
            SupplierId = 1,
            Name = "Name",
            AddressId = 1,
        };

        //Act
        var result = _mapper.Map<SupplierEntity>(command);

        //Assert
        result.Id.Should().Be(command.SupplierId);
        result.Name.Should().Be(command.Name);
        result.AddressId.Should().Be(command.AddressId);
    }

    [Fact]
    public void Should_MapFrom_SupplierContactEntity_To_CreateSupplierContactDto()
    {
        //Arrange
        var command = new SupplierContactEntity()
        {
            Id = 1,
            RepresentativeName = "Patrykos",
            IsCommon = true,
            SupplierId = 1,
            PhoneNumber = "6954934543",
            Email = "email@email.pl",
            Description = "Description",
            IsActive = true,
        };

        //Act
        var result = _mapper.Map<CreateSupplierContactDto>(command);

        //Assert
        result.Id.Should().Be(command.Id);
        result.IsActive.Should().Be(command.IsActive);
        result.IsCommon.Should().Be(command.IsCommon);
        result.RepresentativeName.Should().Be(command.RepresentativeName);
        result.SupplierId.Should().Be(command.SupplierId);
        result.PhoneNumber.Should().Be(command.PhoneNumber);
        result.Description.Should().Be(command.Description);
        result.Email.Should().Be(command.Email);
    }

    [Fact]
    public void Should_MapFrom_CreateSupplierContactDto_To_SupplierContactEntity()
    {
        //Arrange
        var command = new CreateSupplierContactDto()
        {
            Id = 1,
            RepresentativeName = "Patrykos",
            IsCommon = true,
            SupplierId = 1,
            PhoneNumber = "6954934543",
            Email = "email@email.pl",
            Description = "Description",
            IsActive = true,
        };

        //Act
        var result = _mapper.Map<SupplierContactEntity>(command);

        //Assert
        result.Id.Should().Be(command.Id);
        result.IsActive.Should().Be(command.IsActive);
        result.IsCommon.Should().Be(command.IsCommon);
        result.RepresentativeName.Should().Be(command.RepresentativeName);
        result.SupplierId.Should().Be(command.SupplierId);
        result.PhoneNumber.Should().Be(command.PhoneNumber);
        result.Description.Should().Be(command.Description);
        result.Email.Should().Be(command.Email);
    }
}