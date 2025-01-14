using ecms.Application.Handlers.Queries.Product.GetGroupProductMaterialsByFilters;
using ecms.Application.Models.ViewModels.Products;
using ecms.Domain.Entities;
using ecms.Domain.ValueObjects;
using FluentAssertions;
using IntegrationTests.Abstractions;
using SharedKernel;

namespace IntegrationTests.Product.GetGroupProductMaterialsByFilters;

public class GetGroupProductMaterialsByFiltersTests : BaseIntegrationTest
{
    public GetGroupProductMaterialsByFiltersTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
        Seed();
    }

    private void Seed()
    {
        var material = new MaterialEntity
        {
            Name = "DodasekGrubasek",
            MaxStockLevel = 7,
            MinStockLevel = 1,
            UnitOfMeasure = ecms.Domain.Enums.UnitOfMeasureType.Pieces,
            FileGuid = Guid.NewGuid(),
            Description = "Description",
            ReorderLevel = 1,
            IsDeleted = false,
            IsActive = true,
        };

        var category = new CategoryEntity
        {
            HierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId(),
            Name = "TestCategory1",
        };

        var product = new ProductEntity
        {
            Name = "Test1",
            Description = "Test Description",
            AlcoholContent = ecms.Domain.Enums.AlcoholContentType.UpTo4AndAHalfPercentOrBeer,
            Vat = 23,
            Unit = ecms.Domain.Enums.UnitType.Weight,
            CategoryId = 1
        };

        var productVariant = new ProductVariantEntity
        {
            Name = "Variant1",
            Price = new Price(10, Currency.Pln),
            ProductId = 1,
            IsDeleted = false,
        };

        var productMaterial = new ProductMaterialEntity
        {
            ProductVariantId = 1,
            MaterialId = 1,
            IsDeleted = false,
            Quantity = 1,
        };

        ApplicationDbContext.Categories.Add(category);
        ApplicationDbContext.SaveChanges();

        ApplicationDbContext.Products.AddRange(product);
        ApplicationDbContext.SaveChanges();

        ApplicationDbContext.Materials.Add(material);
        ApplicationDbContext.SaveChanges();

        ApplicationDbContext.ProductVariants.Add(productVariant);
        ApplicationDbContext.SaveChanges();

        ApplicationDbContext.ProductMaterials.Add(productMaterial);
        ApplicationDbContext.SaveChanges();
    }

    [Fact]
    public async Task GetGroupProductMaterialsByFiltersQuery_ShouldReturnSuccessResult_OnValidRequest()
    {
        //Arrange
        var query = new GetGroupProductMaterialsByFiltersQuery()
        {
            ProductVariantIds = new List<int> { 1 },
            MaterialIds = new List<int> { 1 }
        };

        //Act
        var result = await Sender.Send(query);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Result<GroupProductMaterialViewModel>>();
        result.IsSuccess.Should().BeTrue();
        result.Value.ProductVariantMaterialGroups.Should().HaveCount(1);
        var firstGroup = result.Value.ProductVariantMaterialGroups.First();
        firstGroup.ProductVariantId.Should().Be(1);
        firstGroup.ProductVariantName.Should().Be("Variant1");
        firstGroup.productMaterialListItems.Should().NotBeEmpty();
        var firstMaterialItem = firstGroup.productMaterialListItems.First();
        firstMaterialItem.ProductMaterialId.Should().Be(1);
        firstMaterialItem.MaterialId.Should().Be(1);
        firstMaterialItem.Name.Should().Be("DodasekGrubasek");
        firstMaterialItem.Quantity.Should().Be(1);
        var orderedMaterials = firstGroup.productMaterialListItems.OrderBy(pm => pm.Name).ToList();

        firstGroup.productMaterialListItems.Should().Equal(orderedMaterials,
            (expected, actual) => expected.Name == actual.Name);
    }
}