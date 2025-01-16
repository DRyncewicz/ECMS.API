using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Queries.Product.GetGroupProductMaterialsByFilters;
using ecms.Domain.Entities;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Queries.Product.GetGroupProductMaterialsByFilters;

public class GetGroupProductMaterialsByFiltersQueryHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly GetGroupProductMaterialsByFiltersQueryHandler _handler;

    public GetGroupProductMaterialsByFiltersQueryHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _handler = new GetGroupProductMaterialsByFiltersQueryHandler(_applicationDbContext.Object, _mapper);

        var productMaterials = new List<ProductMaterialEntity>
        {
            new()
            {
                Id = 1,
                ProductVariant = new ProductVariantEntity
                {
                    Id = 1,
                    Name = "FirstVariant",
                },
                Material = new MaterialEntity
                {
                    Name = "Dodasek1",
                },
                ProductVariantId = 1,
                MaterialId = 2,
                IsDeleted = false,
                Quantity = 1,
            },
            new()
            {
                Id = 2,
                ProductVariant = new ProductVariantEntity
                {
                    Id = 1,
                    Name = "FirstVariant",
                },
                Material = new MaterialEntity
                {
                    Name = "Bobo",
                },
                ProductVariantId = 1,
                MaterialId = 1,
                Quantity = 1,
                IsDeleted = false,
            },
            new()
            {
                Id = 3,
                ProductVariant = new ProductVariantEntity
                {
                    Id = 2,
                    Name = "SecondVariant",
                },
                Material = new MaterialEntity
                {
                    Name = "Bobo",
                },
                MaterialId = 1,
                ProductVariantId = 2,
                IsDeleted = false,
                Quantity = 1,
            },
        };
        var dbContextResponseProductMaterials = productMaterials.AsQueryable().BuildMockDbSet();
        _applicationDbContext.Setup(p => p.ProductMaterials).Returns(dbContextResponseProductMaterials.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenValidRequest()
    {
        // Arrange
        var request = new GetGroupProductMaterialsByFiltersQuery
        {
            ProductVariantIds = new List<int> { 1 },
            MaterialIds = new List<int> { 1, 2 }
        };

        // Act
        var result = await _handler.Handle(request, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.ProductVariantMaterialGroups.Should().HaveCount(1);

        var firstGroup = result.Value.ProductVariantMaterialGroups.First();
        firstGroup.ProductVariantId.Should().Be(1);
        firstGroup.ProductVariantName.Should().Be("FirstVariant");
        firstGroup.productMaterialListItems.Should().NotBeEmpty();

        var orderedMaterials = firstGroup.productMaterialListItems.OrderBy(pm => pm.Name).ToList();
        firstGroup.productMaterialListItems.Should().Equal(orderedMaterials,
            (expected, actual) => expected.Name == actual.Name);

        var firstMaterialItem = firstGroup.productMaterialListItems.First();
        firstMaterialItem.ProductMaterialId.Should().Be(2);
        firstMaterialItem.MaterialId.Should().Be(1);
        firstMaterialItem.Name.Should().Be("Bobo");
        firstMaterialItem.Quantity.Should().Be(1);
    }

    [Fact]
    public async Task Handle_ShouldReturnResult_WhenMultipleVariantsAndMaterialsRequested()
    {
        // Arrange
        var request = new GetGroupProductMaterialsByFiltersQuery
        {
            ProductVariantIds = new List<int> { 1, 2 },
            MaterialIds = new List<int> { 1, 2 }
        };

        // Act
        var result = await _handler.Handle(request, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.ProductVariantMaterialGroups.Should().HaveCount(2);

        var firstGroup = result.Value.ProductVariantMaterialGroups.First(g => g.ProductVariantId == 1);
        firstGroup.ProductVariantId.Should().Be(1);
        firstGroup.ProductVariantName.Should().Be("FirstVariant");
        firstGroup.productMaterialListItems.Should().HaveCount(2);

        var firstMaterialItem1 = firstGroup.productMaterialListItems.First(pm => pm.MaterialId == 2);
        firstMaterialItem1.ProductMaterialId.Should().Be(1);
        firstMaterialItem1.Name.Should().Be("Dodasek1");
        firstMaterialItem1.Quantity.Should().Be(1);

        var firstMaterialItem2 = firstGroup.productMaterialListItems.First(pm => pm.MaterialId == 1);
        firstMaterialItem2.ProductMaterialId.Should().Be(2);
        firstMaterialItem2.Name.Should().Be("Bobo");
        firstMaterialItem2.Quantity.Should().Be(1);

        var secondGroup = result.Value.ProductVariantMaterialGroups.First(g => g.ProductVariantId == 2);
        secondGroup.ProductVariantId.Should().Be(2);
        secondGroup.ProductVariantName.Should().Be("SecondVariant");
        secondGroup.productMaterialListItems.Should().HaveCount(1);

        var secondMaterialItem = secondGroup.productMaterialListItems.First();
        secondMaterialItem.MaterialId.Should().Be(1);
        secondMaterialItem.ProductMaterialId.Should().Be(3);
        secondMaterialItem.Name.Should().Be("Bobo");
        secondMaterialItem.Quantity.Should().Be(1);
    }

    [Fact]
    public async Task Handle_ShouldReturnResult_WhenFilteredByProductVariantIdOnly()
    {
        // Arrange
        var request = new GetGroupProductMaterialsByFiltersQuery
        {
            ProductVariantIds = new List<int> { 2 }
        };

        // Act
        var result = await _handler.Handle(request, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.ProductVariantMaterialGroups.Should().HaveCount(1);

        var firstGroupItem = result.Value.ProductVariantMaterialGroups.First();
        firstGroupItem.ProductVariantId.Should().Be(2);
        firstGroupItem.ProductVariantName.Should().Be("SecondVariant");
        firstGroupItem.productMaterialListItems.Should().NotBeEmpty();

        var materialItem = firstGroupItem.productMaterialListItems.First();
        materialItem.MaterialId.Should().Be(1);
        materialItem.ProductMaterialId.Should().Be(3);
        materialItem.Name.Should().Be("Bobo");
        materialItem.Quantity.Should().Be(1);
        firstGroupItem.productMaterialListItems.Should().HaveCount(1);
    }

    [Fact]
    public async Task Handle_ShouldReturnResult_WhenFilteredByMaterialIdsOnly()
    {
        // Arrange
        var request = new GetGroupProductMaterialsByFiltersQuery
        {
            MaterialIds = new List<int> { 2 }
        };

        // Act
        var result = await _handler.Handle(request, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.ProductVariantMaterialGroups.Should().HaveCount(1);

        var firstGroupItem = result.Value.ProductVariantMaterialGroups.First();
        firstGroupItem.ProductVariantId.Should().Be(1);
        firstGroupItem.ProductVariantName.Should().Be("FirstVariant");

        var materialItem = firstGroupItem.productMaterialListItems.First(pm => pm.MaterialId == 2);
        materialItem.ProductMaterialId.Should().Be(1);
        materialItem.Name.Should().Be("Dodasek1");
        materialItem.Quantity.Should().Be(1);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyResult_WhenNoMatchingProductVariantsOrMaterials()
    {
        // Arrange
        var request = new GetGroupProductMaterialsByFiltersQuery
        {
            ProductVariantIds = new List<int> { 3 },
            MaterialIds = new List<int> { 3 }
        };

        // Act
        var result = await _handler.Handle(request, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.ProductVariantMaterialGroups.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyResult_WhenNoMatchingProductVariantIds()
    {
        // Arrange
        var request = new GetGroupProductMaterialsByFiltersQuery
        {
            ProductVariantIds = new List<int> { 3 },
            MaterialIds = new List<int> { 1 }
        };

        // Act
        var result = await _handler.Handle(request, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.ProductVariantMaterialGroups.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyResult_WhenNoMatchingMaterialIds()
    {
        // Arrange
        var request = new GetGroupProductMaterialsByFiltersQuery
        {
            ProductVariantIds = new List<int> { 1 },
            MaterialIds = new List<int> { 3 }
        };

        // Act
        var result = await _handler.Handle(request, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.ProductVariantMaterialGroups.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_ShouldReturnAll_WhenQueryIsEmpty()
    {
        // Arrange
        var request = new GetGroupProductMaterialsByFiltersQuery();

        // Act
        var result = await _handler.Handle(request, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.ProductVariantMaterialGroups.Should().NotBeEmpty();
        result.Value.ProductVariantMaterialGroups.Should().HaveCount(2);

        var firstGroup = result.Value.ProductVariantMaterialGroups.First(g => g.ProductVariantId == 1);
        firstGroup.ProductVariantId.Should().Be(1);
        firstGroup.ProductVariantName.Should().Be("FirstVariant");
        firstGroup.productMaterialListItems.Should().HaveCount(2);

        var firstMaterialItem = firstGroup.productMaterialListItems.First(pm => pm.MaterialId == 2);
        firstMaterialItem.ProductMaterialId.Should().Be(1);
        firstMaterialItem.Name.Should().Be("Dodasek1");
        firstMaterialItem.Quantity.Should().Be(1);

        var secondMaterialItem = firstGroup.productMaterialListItems.First(pm => pm.MaterialId == 1);
        secondMaterialItem.ProductMaterialId.Should().Be(2);
        secondMaterialItem.Name.Should().Be("Bobo");
        secondMaterialItem.Quantity.Should().Be(1);

        var secondGroup = result.Value.ProductVariantMaterialGroups.First(g => g.ProductVariantId == 2);
        secondGroup.ProductVariantId.Should().Be(2);
        secondGroup.ProductVariantName.Should().Be("SecondVariant");
        secondGroup.productMaterialListItems.Should().HaveCount(1);
        secondGroup.productMaterialListItems.First().Name.Should().Be("Bobo");
        secondGroup.productMaterialListItems.First().Quantity.Should().Be(1);
    }
}