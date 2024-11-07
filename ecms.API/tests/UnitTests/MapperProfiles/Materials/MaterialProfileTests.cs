using AutoMapper;
using ecms.Application.Handlers.Commands.CreateMaterial;
using ecms.Domain.Entities;
using FluentAssertions;
using UnitTests.Mapping;

public class MaterialProfileTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;

    public MaterialProfileTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
    }

    [Fact]
    public void Should_MapFrom_CreateMaterialCommand_To_MaterialEntity()
    {
        //Arrange
        var command = new CreateMaterialCommand()
        {
            Name = "Name",
            UnitOfMeasure = ecms.Domain.Enums.UnitOfMeasureType.Pieces,
            Description = "Description",
            MinStockLevel = 1,
            MaxStockLevel = 7,
            ReorderLevel = 1,
            FileGuid = Guid.NewGuid(),
            IsActive = true,
            IsDeleted = false,
        };

        //Act
        var result = _mapper.Map<MaterialEntity>(command);

        //Assert
        result.Name.Should().Be(command.Name);
        result.FileGuid.Should().Be(command.FileGuid);
        result.Description.Should().Be(command.Description);
        result.MinStockLevel.Should().Be(command.MinStockLevel);
        result.MaxStockLevel.Should().Be(command.MaxStockLevel);
        result.ReorderLevel.Should().Be(command.ReorderLevel);
        result.FileGuid.Should().Be(command.FileGuid);
        result.IsActive.Should().Be(command.IsActive);
        result.IsDeleted.Should().Be(command.IsDeleted);
    }

    [Fact]
    public void Should_MapFrom_CreateMaterialCommand_To_StockLevelEntity()
    {
        //Arrange
        var command = new CreateMaterialCommand()
        {
            StockId = 1,
            BatchNumber = "DodasekGrubasek",
            IsDeleted = false,            
        };

        //Act
        var result = _mapper.Map<StockLevelEntity>(command);

        //Assert
        result.StockId.Should().Be(command.StockId);
        result.BatchNumber.Should().Be(command.BatchNumber);
        result.Quantity.Should().Be(0);
        result.IsDeleted.Should().Be(false);     
    }

    [Fact]
    public void Should_MapFrom_MaterialEntity_To_MaterialHistoryEntity()
    {
        //Arrange
        var command = new MaterialEntity()
        {
            Id = 1,
            Name = "DodasekGrubasek",
            MaxStockLevel = 7,
            MinStockLevel = 1,
            UnitOfMeasure = ecms.Domain.Enums.UnitOfMeasureType.Pieces,
            FileGuid = Guid.NewGuid(),
            Description = "Description",
            ReorderLevel = 1,
        };

        //Act
        var result = _mapper.Map<MaterialHistoryEntity>(command);

        //Assert
        result.IsActive.Should().Be(true);
        result.IsDeleted.Should().Be(false);
        result.Name.Should().Be(command.Name);
        result.MaxStockLevel.Should().Be(command.MaxStockLevel);
        result.MinStockLevel.Should().Be(command.MinStockLevel);
        result.UnitOfMeasure.Should().Be(command.UnitOfMeasure);
        result.FileGuid.Should().Be(command.FileGuid);
        result.Description.Should().Be(command.Description);
        result.ReorderLevel.Should().Be(command.ReorderLevel);
        result.MaterialId.Should().Be(command.Id);
    }
}