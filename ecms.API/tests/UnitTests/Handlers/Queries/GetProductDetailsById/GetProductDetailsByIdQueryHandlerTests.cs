using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Queries.GetProductDetailsById;
using ecms.Domain.Entities;
using FluentAssertions;
using Moq;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Queries.GetProductDetailsById;

public class GetProductDetailsByIdQueryHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly GetProductDetailsByIdQueryHandler _handler;

    public GetProductDetailsByIdQueryHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _handler = new GetProductDetailsByIdQueryHandler(_applicationDbContext.Object, _mapper);

        var allergens = new List<AllergenEntity>();
        var dbContextResponseAllergens = allergens.AsQueryable().BuildMock();
        _applicationDbContext.Setup(p => p.Allergens).Returns(dbContextResponseAllergens.Object);

        var productVariantAllergen = new List<ProductVariantAllergenEntity>
        {
            new ProductVariantAllergenEntity
            {
                Allergen = new AllergenEntity(),
                AllergenId = 0,
                Id = 1,
                ProductVariantId = 1,
            },
            new ProductVariantAllergenEntity
            {
                Allergen = new AllergenEntity(),
                AllergenId = 0,
                Id = 2,
                ProductVariantId = 1,
            },
        };
        var dbContextResponseVariantAllergens = productVariantAllergen.AsQueryable().BuildMock();
        _applicationDbContext.Setup(p => p.ProductVariantAllergens).Returns(dbContextResponseVariantAllergens.Object);

        var categories = new List<CategoryEntity>
        {
            new CategoryEntity
            {
            Id = 1,
            HierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId("/"),
            Name = "Main",
            }
        };
        var dbContextResponseCategories = categories.AsQueryable().BuildMock();
        _applicationDbContext.Setup(p => p.Categories).Returns(dbContextResponseCategories.Object);

        var productVariants = new List<ProductVariantEntity>
        {
            new ProductVariantEntity
            {
                ProductId = 1,
                Id = 1,
                Name = "FirstVariant",
                IsDeleted = false,
                Price = new ecms.Domain.ValueObjects.Price(1, ecms.Domain.ValueObjects.Currency.Usd),
                ProductVariantAllergens = productVariantAllergen
            },
            new ProductVariantEntity
            {
                ProductId = 1,
                Id = 2,
                Name = "SecondVariant",
                IsDeleted = false,
                Price = new ecms.Domain.ValueObjects.Price(1, ecms.Domain.ValueObjects.Currency.Usd),
                ProductVariantAllergens = [],
            }
        };
        var dbContextResponseVariants = productVariants.AsQueryable().BuildMock();
        _applicationDbContext.Setup(p => p.ProductVariants).Returns(dbContextResponseVariants.Object);

        var products = new List<ProductEntity>
        {
             new ProductEntity
             {
                 Id = 1,
                 Name = "Test1",
                 Description = "Test Description",
                 AlcoholContent = ecms.Domain.Enums.AlcoholContentType.UpTo4AndAHalfPercentOrBeer,
                 Vat = 23,
                 Unit = ecms.Domain.Enums.UnitType.Weight,
                 CategoryId = 1,
                 ProductVariants = productVariants,
             },
             new ProductEntity
             {
                 Id = 2,
                 Name = "Test2",
                 Description = "Test Description",
                 AlcoholContent = ecms.Domain.Enums.AlcoholContentType.UpTo4AndAHalfPercentOrBeer,
                 Vat = 23,
                 Unit = ecms.Domain.Enums.UnitType.Weight,
                 CategoryId = 1
             },
        };
        var dbContextResponseProducts = products.AsQueryable().BuildMock();
        _applicationDbContext.Setup(p => p.Products).Returns(dbContextResponseProducts.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenProductExists()
    {
        //Arrange
        var query = new GetProductDetailsByIdQuery(1);

        //Act
        var result = await _handler.Handle(query, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.ProductId.Should().Be(1);
        result.Value.Description.Should().Be("Test Description");
        result.Value.AlcoholContent.Should().Be(ecms.Domain.Enums.AlcoholContentType.UpTo4AndAHalfPercentOrBeer);
        result.Value.Vat.Should().Be(23);
        result.Value.Unit.Should().Be(ecms.Domain.Enums.UnitType.Weight);
        result.Value.productVariantDtos.Should().HaveCount(2);
        result.Value.CategoryId.Should().Be(1);
        result.Value.Name.Should().Be("Test1");
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenProductDoesntExist()
    {
        //Arrange
        var query = new GetProductDetailsByIdQuery(10);

        //Act
        Func<Task> act = async () => await _handler.Handle(query, default);

        //Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }
}