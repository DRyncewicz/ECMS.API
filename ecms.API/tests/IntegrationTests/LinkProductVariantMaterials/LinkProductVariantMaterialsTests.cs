using ecms.Application.Handlers.Commands.LinkProductVariantMaterials;
using ecms.Application.Models.Dtos.Materials;
using ecms.Domain.Entities;
using FluentAssertions;
using IntegrationTests.Abstractions;

namespace IntegrationTests.LinkProductVariantMaterials;

public class LinkProductVariantMaterialsTests : BaseIntegrationTest
{
    public LinkProductVariantMaterialsTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
        Seed();
    }

    private void Seed()
    {
        var products = new List<ProductEntity>
        {
             new ProductEntity
             {
                 Name = "Test1",
                 Description = "Test Description",
                 AlcoholContent = ecms.Domain.Enums.AlcoholContentType.UpTo4AndAHalfPercentOrBeer,
                 Vat = 23,
                 Unit = ecms.Domain.Enums.UnitType.Weight,
                 CategoryId = 1
             },
             new ProductEntity
             {
                 Name = "Test2",
                 Description = "Test Description",
                 AlcoholContent = ecms.Domain.Enums.AlcoholContentType.UpTo4AndAHalfPercentOrBeer,
                 Vat = 23,
                 Unit = ecms.Domain.Enums.UnitType.Weight,
                 CategoryId = 1
             },
             new ProductEntity
             {
                 Name = "Test3",
                 Description = "Test Description",
                 AlcoholContent = ecms.Domain.Enums.AlcoholContentType.UpTo4AndAHalfPercentOrBeer,
                 Vat = 23,
                 Unit = ecms.Domain.Enums.UnitType.Weight,
                 CategoryId = 1
             }
        };

        var productVariant = new ProductVariantEntity()
        {
            Name = "variant",
            Price = new ecms.Domain.ValueObjects.Price(10, ecms.Domain.ValueObjects.Currency.Usd),
            ProductId = 1,
            IsDeleted = false,
        };

        var category = new CategoryEntity
        {
            HierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId(),
            Name = "TestCategory1",
        };

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

        ApplicationDbContext.Categories.Add(category);
        ApplicationDbContext.SaveChanges();

        ApplicationDbContext.Products.AddRange(products);
        ApplicationDbContext.SaveChanges();

        ApplicationDbContext.ProductVariants.Add(productVariant);
        ApplicationDbContext.SaveChanges();

        ApplicationDbContext.Materials.Add(material);
        ApplicationDbContext.SaveChanges();
    }

    [Fact]
    public async Task LinkProductVariantMaterials_ShouldUpdateProductMaterials_OnValidRequest()
    {
        //Arrange
        var command = new LinkProductVariantMaterialsCommand()
        {
            ProductVariantId = 1,
            ProductMaterialDtos = new List<ProductMaterialDto>
            {
                new()
                {
                    MaterialId = 1,
                    Quantity = 10
                }
            }
        };

        //Act
        var result = await Sender.Send(command);

        //Assert
        result.IsSuccess.Should().BeTrue();
        var productMaterial = ApplicationDbContext.ProductMaterials.First(p => p.ProductVariantId == 1);
        productMaterial.Quantity.Should().Be(10);
        productMaterial.MaterialId.Should().Be(1);
    }
}