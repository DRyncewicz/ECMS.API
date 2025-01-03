using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Queries.Material.GetMaterialsByFilters;
using ecms.Domain.Entities;
using FluentAssertions;
using Moq;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Queries.Material.GetMaterialsByFilters;

public class GetMaterialsByFiltersQueryHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly GetMaterialsByFiltersQueryHandler _handler;
    private readonly Guid fileGuid = Guid.NewGuid();

    public GetMaterialsByFiltersQueryHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _handler = new GetMaterialsByFiltersQueryHandler(_applicationDbContext.Object, _mapper);
        var materials = new List<MaterialEntity>
        {
            new MaterialEntity()
            {
                Id = 1,
                Name = "DodasekGrubasek",
                MaxStockLevel = 7,
                MinStockLevel = 1,
                UnitOfMeasure = ecms.Domain.Enums.UnitOfMeasureType.Pieces,
                FileGuid = fileGuid,
                Description = "Description",
                ReorderLevel = 1,
                IsDeleted = false,
                IsActive = true,
                StockLevel = new StockLevelEntity
                {
                    Id = 1,
                    BatchNumber = "BatchNumber",
                    CreateDateTimeUtc = new DateTime(2024, 9, 22),
                    IsDeleted = false,
                    LastUpdated = new DateTime(2024, 9, 22),
                    MaterialId = 1,
                    Quantity = 1,
                    StockId = 1,
                }
            },
            new MaterialEntity()
            {
                Id = 2,
                Name = "DodasekGrubasek",
                MaxStockLevel = 7,
                MinStockLevel = 1,
                UnitOfMeasure = ecms.Domain.Enums.UnitOfMeasureType.Pieces,
                FileGuid = fileGuid,
                Description = "Description",
                ReorderLevel = 1,
                IsDeleted = false,
                IsActive = true,
                StockLevel = new StockLevelEntity
                {
                    Id = 1,
                    BatchNumber = "BatchNumber2",
                    CreateDateTimeUtc = new DateTime(2024, 9, 22),
                    IsDeleted = false,
                    LastUpdated = new DateTime(2024, 9, 22),
                    MaterialId = 1,
                    Quantity = 1,
                    StockId = 1,
                }
            },
            new MaterialEntity()
            {
                Id = 3,
                Name = "DodasekGrubasek",
                MaxStockLevel = 7,
                MinStockLevel = 1,
                UnitOfMeasure = ecms.Domain.Enums.UnitOfMeasureType.Pieces,
                FileGuid = fileGuid,
                Description = "Description",
                ReorderLevel = 1,
                IsDeleted = false,
                IsActive = false,
                StockLevel = new StockLevelEntity
                {
                    Id = 1,
                    BatchNumber = "BatchNumber3",
                    CreateDateTimeUtc = new DateTime(2024, 9, 22),
                    IsDeleted = false,
                    LastUpdated = new DateTime(2024, 9, 22),
                    MaterialId = 1,
                    Quantity = 1,
                    StockId = 1,
                }
            }
        };
        var dbContextResponseMaterials = materials.AsQueryable().BuildMock();
        _applicationDbContext.Setup(p => p.Materials).Returns(dbContextResponseMaterials.Object);
    }

    [Fact]
    public async Task Handle_EmptyQueryShouldReturnAllRecords()
    {
        //Arrange
        var request = new GetMaterialsByFiltersQuery();

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        result.Value.TotalCount.Should().Be(3);
        result.Value.Materials.Should().HaveCount(3);
    }

    [Fact]
    public async Task Handle_ShouldReturnRecordWithAllFilters()
    {
        //Arrange
        var request = new GetMaterialsByFiltersQuery()
        {
            Name = "DodasekGrubasek",
            BatchNumber = "BatchNumber",
            OnlyActive = true,
            CurrentPage = 1,
            PageSize = 5
        };

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        result.Value.TotalCount.Should().Be(2);
        result.Value.Materials.Should().HaveCount(2);
    }

    [Fact]
    public async Task Handle_ShouldReturnRecordWithName()
    {
        //Arrange
        var request = new GetMaterialsByFiltersQuery()
        {
            Name = "DodasekGrubasek"
        };

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        result.Value.Materials.First().Name.Should().Be("DodasekGrubasek");
        result.Value.TotalCount.Should().Be(3);
        result.Value.Materials.Should().HaveCount(3);
    }

    [Fact]
    public async Task Handle_ShouldReturnRecordWithBatchNumber()
    {
        //Arrange
        var request = new GetMaterialsByFiltersQuery()
        {
            BatchNumber = "BatchNumber3"
        };

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        result.Value.TotalCount.Should().Be(1);
        result.Value.Materials.Should().HaveCount(1);
    }

    [Fact]
    public async Task Handle_ShouldReturnRecordWithOnlyActive()
    {
        //Arrange
        var request = new GetMaterialsByFiltersQuery()
        {
            OnlyActive = true
        };

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        result.Value.TotalCount.Should().Be(2);
        result.Value.Materials.Should().HaveCount(2);
    }

    [Fact]
    public async Task Handle_ShouldReturnRecordWithPagination()
    {
        //Arrange
        var request = new GetMaterialsByFiltersQuery()
        {
            CurrentPage = 1,
            PageSize = 2
        };

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        result.Value.Materials.Should().HaveCount(2);
        result.Value.TotalCount.Should().Be(3);
    }

    [Fact]
    public async Task Handle_ShouldReturnRecordWithValidPagination()
    {
        //Arrange
        var request = new GetMaterialsByFiltersQuery()
        {
            CurrentPage = 2,
            PageSize = 5
        };

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        result.Value.Materials.Should().HaveCount(0);
        result.Value.TotalCount.Should().Be(3);
    }
}

