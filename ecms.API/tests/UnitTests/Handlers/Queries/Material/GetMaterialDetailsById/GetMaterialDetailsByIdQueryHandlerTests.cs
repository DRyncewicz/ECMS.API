using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Queries.Material.GetMaterialDetailsById;
using ecms.Domain.Entities;
using FluentAssertions;
using Moq;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Queries.Material.GetMaterialDetailsById;

public class GetMaterialDetailsByIdQueryHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly GetMaterialDetailsByIdQueryHandler _handler;
    private readonly Guid fileGuid = Guid.NewGuid();

    public GetMaterialDetailsByIdQueryHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _handler = new GetMaterialDetailsByIdQueryHandler(_applicationDbContext.Object, _mapper);

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
            }
        };
        var dbContextResponseMaterials = materials.AsQueryable().BuildMock();
        _applicationDbContext.Setup(p => p.Materials).Returns(dbContextResponseMaterials.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenMaterialExists()
    {
        //Arrange
        var query = new GetMaterialDetailsByIdQuery(1);

        //Act
        var result = await _handler.Handle(query, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.MaterialId.Should().Be(1);
        result.Value.Name.Should().Be("DodasekGrubasek");
        result.Value.MaxStockLevel.Should().Be(7);
        result.Value.MinStockLevel.Should().Be(1);
        result.Value.UnitOfMeasure.Should().Be(ecms.Domain.Enums.UnitOfMeasureType.Pieces);
        result.Value.FileGuid.Should().Be(fileGuid);
        result.Value.Description.Should().Be("Description");
        result.Value.ReorderLevel.Should().Be(1);
        result.Value.IsActive.Should().Be(true);
        result.Value.StockLevel.BatchNumber.Should().Be("BatchNumber");
        result.Value.StockLevel.StockLevelId.Should().Be(1);
        result.Value.StockLevel.LastUpdated.Should().Be(new DateTime(2024, 9, 22));
        result.Value.StockLevel.Quantity.Should().Be(1);
        result.Value.StockLevel.StockId.Should().Be(1);
    }

    [Fact]
    public async Task Handle_ShouldEarlyReturnResultFailure_WhenMaterialDoesntExist()
    {
        //Arrange
        var query = new GetMaterialDetailsByIdQuery(10);

        //Act
        var result = await _handler.Handle(query, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().NotBeNull();
    }
}