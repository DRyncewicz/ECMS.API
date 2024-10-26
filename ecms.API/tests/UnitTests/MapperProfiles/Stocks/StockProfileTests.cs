using AutoMapper;
using ecms.Application.Handlers.Commands.CreateStock;
using ecms.Application.Handlers.Commands.EditStock;
using ecms.Domain.Entities;
using FluentAssertions;
using UnitTests.Mapping;

namespace ecms.Application.Tests.MapperProfiles.Stocks
{
    public class StockProfileTests : IClassFixture<MappingTestFixture>
    {
        private readonly IMapper _mapper;

        public StockProfileTests(MappingTestFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void Should_Map_CreateStockCommand_To_StockEntity()
        {
            // Arrange
            var command = new CreateStockCommand
            {
                AddressId = 1,
                Name = "Dups",
                Description = "Dupa",
            };

            // Act
            var result = _mapper.Map<StockEntity>(command);

            // Assert
            result.Should().NotBeNull();
            result.AddressId.Should().Be(command.AddressId);
            result.Name.Should().Be(command.Name);
            result.Description.Should().Be(command.Description);
        }

        [Fact]
        public void Should_Map_EditStockCommand_To_StockEntity()
        {
            // Arrange
            var command = new EditStockCommand
            {
                AddressId = 1,
                Name = "Dupa",
                Description = "Dupa",
                StockId = 1
            };

            // Act
            var result = _mapper.Map<StockEntity>(command);

            // Assert
            result.Should().NotBeNull();
            result.AddressId.Should().Be(command.AddressId);
            result.Name.Should().Be(command.Name);
            result.Description.Should().Be(command.Description);
            result.Id.Should().Be(command.StockId);
        }
    }
}