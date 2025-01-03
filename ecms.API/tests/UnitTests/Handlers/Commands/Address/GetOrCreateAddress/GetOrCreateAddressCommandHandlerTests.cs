using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Commands.Address.GetOrCreateAddress;
using ecms.Domain.Entities;
using FluentAssertions;
using Moq;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Commands.Address.GetOrCreateAddress;

public class GetOrCreateAddressCommandHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly GetOrCreateAddressCommandHandler _handler;

    private readonly List<AddressEntity> _addresses = new List<AddressEntity>
        {
            new AddressEntity
            {
                Id = 1,
                Country = "Poland",
                City = "Koszalin",
                Street = "Rodła",
                PostalCode = "75-361",
                BuildingNumber = "42",
                ApartmentNumber = "",
            },
            new ()
            {
                Id = 2,
                Country = "Poland",
                City = "Koszalin",
                Street = "Rodła",
                PostalCode = "75-361",
                BuildingNumber = "41",
                ApartmentNumber = "",
            },
        };

    public GetOrCreateAddressCommandHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _handler = new GetOrCreateAddressCommandHandler(_applicationDbContext.Object, _mapper);
        var dbContextResponse = _addresses.AsQueryable().BuildMock();
        _applicationDbContext.Setup(p => p.Addresses).Returns(dbContextResponse.Object);
    }

    [Fact]
    public async Task Handle_WhenAddressExists_ReturnsExistingAddressId()
    {
        // Arrange
        var command = new GetOrCreateAddressCommand
        {
            Country = "Poland",
            City = "Koszalin",
            Street = "Rodła",
            PostalCode = "75-361",
            BuildingNumber = "42",
            ApartmentNumber = "",
        };

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(1);
    }

    [Fact]
    public async Task Handle_WhenAddressDoesNotExist_CreatesNewAddressAndReturnsId()
    {
        // Arrange
        var command = new GetOrCreateAddressCommand
        {
            Country = "Poland",
            City = "Koszalin",
            Street = "Jana Z Kolana",
            PostalCode = "75-361",
            BuildingNumber = "32",
            ApartmentNumber = "24"
        };

        _applicationDbContext.Setup(p => p.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        _applicationDbContext.Verify(db => db.Addresses.AddAsync(It.IsAny<AddressEntity>(), It.IsAny<CancellationToken>()), Times.Once);
        _applicationDbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}