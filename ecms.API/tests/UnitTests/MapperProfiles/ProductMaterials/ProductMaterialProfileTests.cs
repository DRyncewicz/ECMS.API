using AutoMapper;
using ecms.Application.Models.Dtos.Materials;
using ecms.Domain.Entities;
using FluentAssertions;
using UnitTests.Mapping;

namespace UnitTests.MapperProfiles.ProductMaterials;

public class ProductMaterialProfileTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;

    public ProductMaterialProfileTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
    }

    [Fact]
    public void Should_MapFrom_ProductMaterialEntity_To_ProductMaterialDto()
    {
        //Arrange
        var command = new ProductMaterialEntity()
        {
            MaterialId = 1,
            Quantity = 12,
        };

        //Act
        var result = _mapper.Map<ProductMaterialDto>(command);

        //Assert
        result.MaterialId.Should().Be(command.MaterialId);
        result.Quantity.Should().Be(command.Quantity);
    }

    [Fact]
    public void Should_MapFrom_ProductMaterialEntity_To_ProductMaterialHistoryEntity()
    {
        //Arrange
        var command = new ProductMaterialEntity()
        {
            Id = 1,
            MaterialId = 1,
            Quantity = 12,
            ProductVariantId = 1,
            IsDeleted = true,
        };

        //Act
        var result = _mapper.Map<ProductMaterialHistoryEntity>(command);

        //Assert
        result.ProductMaterialId.Should().Be(command.Id);
        result.ProductVariantId.Should().Be(command.ProductVariantId);
        result.MaterialId.Should().Be(command.MaterialId);
        result.IsDeleted.Should().Be(command.IsDeleted);
        result.Quantity.Should().Be(command.Quantity);
    }

    [Fact]
    public void Should_MapFrom_ProductMaterialDto_To_ProductMaterialEntity()
    {
        //Arrange
        var command = new ProductMaterialDto()
        {
            MaterialId = 1,
            Quantity = 12,
        };

        //Act
        var result = _mapper.Map<ProductMaterialEntity>(command);

        //Assert
        result.MaterialId.Should().Be(command.MaterialId);
        result.IsDeleted.Should().Be(false);
        result.Quantity.Should().Be(command.Quantity);
    }
}