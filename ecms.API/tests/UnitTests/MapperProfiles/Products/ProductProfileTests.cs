using AutoMapper;
using ecms.Application.Handlers.Commands.CreateProduct;
using ecms.Application.Handlers.Commands.EditProduct;
using ecms.Application.Models.Dtos.Allergens;
using ecms.Application.Models.Dtos.Products;
using ecms.Domain.Entities;
using FluentAssertions;
using UnitTests.Mapping;

namespace ecms.Application.Tests.MapperProfiles.Products
{
    public class ProductProfileTests : IClassFixture<MappingTestFixture>
    {
        private readonly IMapper _mapper;

        public ProductProfileTests(MappingTestFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void Should_Map_ProductVariantEntity_To_ProductVariantDto()
        {
            // Arrange
            var productVariantEntity = new ProductVariantEntity
            {
                Id = 1,
                ProductId = 1,
                Name = "Test Variant",
                Price = new Domain.ValueObjects.Price(10, Domain.ValueObjects.Currency.Pln)
            };

            // Act
            var result = _mapper.Map<ProductVariantDto>(productVariantEntity);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(productVariantEntity.Id);
            result.ProductId.Should().Be(productVariantEntity.ProductId);
            result.Name.Should().Be(productVariantEntity.Name);
            result.Price.Should().Be(productVariantEntity.Price);
        }

        [Fact]
        public void Should_Map_ProductEntity_To_ProductDto()
        {
            // Arrange
            var productEntity = new ProductEntity
            {
                Id = 1,
                FileGuid = Guid.NewGuid(),
                Name = "Test Product",
                CategoryId = 1,
                Vat = 23,
                ProductVariants = new List<ProductVariantEntity>
                {
                    new ProductVariantEntity { Id = 1, Name = "Variant 1", Price = new Domain.ValueObjects.Price(10, Domain.ValueObjects.Currency.Pln) }
                }
            };

            // Act
            var result = _mapper.Map<ProductDto>(productEntity);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(productEntity.Id);
            result.FileGuid.Should().Be(productEntity.FileGuid);
            result.Name.Should().Be(productEntity.Name);
            result.CategoryId.Should().Be(productEntity.CategoryId);
            result.Vat.Should().Be(productEntity.Vat);
            result.ProductVariants.Should().HaveCount(1);
        }

        [Fact]
        public void Should_Map_EditProductCommand_To_ProductEntity()
        {
            // Arrange
            var command = new EditProductCommand
            {
                Id = 1,
                FileGuid = Guid.NewGuid(),
                Name = "Updated Product",
                CategoryId = 1,
                AlcoholContent = Domain.Enums.AlcoholContentType.AlcoholFree,
                Unit = Domain.Enums.UnitType.Piece,
                GtuCode = Domain.Enums.GtuCodeType.AlcoholDrinks01,
                Description = "Updated Description",
                Vat = 23
            };

            // Act
            var result = _mapper.Map<ProductEntity>(command);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(command.Id);
            result.FileGuid.Should().Be(command.FileGuid);
            result.Name.Should().Be(command.Name);
            result.CategoryId.Should().Be(command.CategoryId);
            result.AlcoholContent.Should().Be(command.AlcoholContent);
        }

        [Fact]
        public void Should_Map_CreateProductCommand_To_ProductEntity()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                FileGuid = Guid.NewGuid(),
                Name = "New Product",
                CategoryId = 2,
                AlcoholContent = Domain.Enums.AlcoholContentType.AlcoholFree,
                Unit = Domain.Enums.UnitType.Piece,
                GtuCode = Domain.Enums.GtuCodeType.AlcoholDrinks01,
                Description = "New Description",
                Vat = 23
            };

            // Act
            var result = _mapper.Map<ProductEntity>(command);

            // Assert
            result.Should().NotBeNull();
            result.FileGuid.Should().Be(command.FileGuid);
            result.Name.Should().Be(command.Name);
        }

        [Fact]
        public void Should_Map_CreateProductVariantDto_To_ProductVariantEntity()
        {
            // Arrange
            var productDto = new CreateProductVariantDto
            {
                Name = "New Variant",
                Price = new Domain.ValueObjects.Price(10, Domain.ValueObjects.Currency.Pln),
            };

            // Act
            var result = _mapper.Map<ProductVariantEntity>(productDto);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(productDto.Name);
            result.Price.Should().Be(productDto.Price);
        }

        [Fact]
        public void Should_Map_ProductEntity_To_ProductHistoryEntity()
        {
            // Arrange
            var productEntity = new ProductEntity
            {
                Id = 1,
                FileGuid = Guid.NewGuid(),
                Name = "Test Product",
                CategoryId = 1,
                AlcoholContent = Domain.Enums.AlcoholContentType.AlcoholFree,
                Unit = Domain.Enums.UnitType.Piece,
                GtuCode = Domain.Enums.GtuCodeType.AlcoholDrinks01,
                Description = "Description",
                Vat = 23,
                IsDeleted = false
            };

            // Act
            var historyResult = _mapper.Map<ProductHistoryEntity>(productEntity);

            // Assert
            historyResult.Should().NotBeNull();
            historyResult.ProductId.Should().Be(productEntity.Id);
            historyResult.FileGuid.Should().Be(productEntity.FileGuid);
            historyResult.Name.Should().Be(productEntity.Name);
            historyResult.CategoryId.Should().Be(productEntity.CategoryId);
            historyResult.AlcoholContent.Should().Be(productEntity.AlcoholContent);
            historyResult.Unit.Should().Be(productEntity.Unit);
            historyResult.GtuCode.Should().Be(productEntity.GtuCode);
            historyResult.Description.Should().Be(productEntity.Description);
            historyResult.Vat.Should().Be(productEntity.Vat);
            historyResult.IsDeleted.Should().Be(productEntity.IsDeleted);
        }

        [Fact]
        public void Should_Map_ProductVariantEntity_To_ProductVariantHistoryEntity()
        {
            // Arrange
            var variantEntity = new ProductVariantEntity
            {
                Id = 1,
                ProductId = 1,
                Name = "Test Variant",
                Price = new Domain.ValueObjects.Price(10, Domain.ValueObjects.Currency.Pln),
                IsDeleted = false
            };

            // Act
            var variantHistoryResult = _mapper.Map<ProductVariantHistoryEntity>(variantEntity);

            // Assert
            variantHistoryResult.Should().NotBeNull();
            variantHistoryResult.ProductVariantId.Should().Be(variantEntity.Id);
            variantHistoryResult.ProductId.Should().Be(variantEntity.ProductId);
            variantHistoryResult.Name.Should().Be(variantEntity.Name);
            variantHistoryResult.Price.Should().Be(variantEntity.Price);
            variantHistoryResult.IsDeleted.Should().Be(variantEntity.IsDeleted);
        }

        [Fact]
        public void Should_Map_AllergenEntity_To_AllergenDto()
        {
            // Arrange
            var allergenEntity = new AllergenEntity
            {
                Name = "New Variant",
                Id = 1,
            };

            // Act
            var result = _mapper.Map<AllergenDto>(allergenEntity);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(allergenEntity.Name);
            result.AllergenId.Should().Be(allergenEntity.Id);
        }
    }
}