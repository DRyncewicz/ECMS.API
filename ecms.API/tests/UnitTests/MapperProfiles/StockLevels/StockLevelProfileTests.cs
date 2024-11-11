using AutoMapper;
using ecms.Application.Handlers.Commands.CreateMaterial;
using ecms.Application.Models.Dtos.StockLevels;
using ecms.Domain.Entities;
using FluentAssertions;
using UnitTests.Mapping;

namespace UnitTests.MapperProfiles.StockLevels;

public class StockLevelProfileTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;

    public StockLevelProfileTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
    }

    [Fact]
    public void Should_MapFrom_StockLevelEntity_To_StockLevelDto()
    {
        //Arrange
        var command = new StockLevelEntity
        {
            BatchNumber = "BatchNumber",
            CreateDateTimeUtc = DateTime.Now,
            IsDeleted = false,
            LastUpdated = DateTime.Now,
            MaterialId = 1,
            Quantity = 1,
            StockId = 1,
        };

        //Act
        var result = _mapper.Map<StockLevelDto>(command);

        //Assert
        result.BatchNumber.Should().Be(command.BatchNumber);
        result.StockId.Should().Be(command.StockId);
        result.StockLevelId.Should().Be(command.Id);
        result.LastUpdated.Should().Be(command.LastUpdated);
        result.Quantity.Should().Be(command.Quantity);
    }
}
