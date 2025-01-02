using ecms.Application.Handlers.Commands.EditProduct;
using ecms.Application.Models.Dtos.Products;
using ecms.Domain.Entities;
using FluentAssertions;
using IntegrationTests.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.Product.EditProduct;

public class EditProductTests : BaseIntegrationTest
{
    public EditProductTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
        Seed();
    }

    private void Seed()
    {
        var category = new CategoryEntity
        {
            HierarchyId = new HierarchyId(),
            Name = "TestCategory1",
        };

        var product = new ProductEntity
        {
            Name = "Name",
            CategoryId = 1,
            AlcoholContent = ecms.Domain.Enums.AlcoholContentType.AlcoholFree,
            Description = "TestDescription",
            IsDeleted = false,
            Unit = ecms.Domain.Enums.UnitType.Piece,
            Vat = 23,
        };

        var productVariant = new ProductVariantEntity
        {
            ProductId = 1,
            Name = "Name",
            Price = new ecms.Domain.ValueObjects.Price(12, ecms.Domain.ValueObjects.Currency.Eur)
        };

        var productVariant2 = new ProductVariantEntity
        {
            ProductId = 1,
            Name = "Name2",
            Price = new ecms.Domain.ValueObjects.Price(15, ecms.Domain.ValueObjects.Currency.Eur)
        };

        ApplicationDbContext.Categories.Add(category);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.Products.Add(product);
        ApplicationDbContext.SaveChanges();
        ApplicationDbContext.ProductVariants.AddRange(productVariant, productVariant2);
        ApplicationDbContext.SaveChanges();
    }

    [Fact]
    public async Task EditProductCommand_ShouldEditProduct_OnValidRequest()
    {
        //Arrange
        var command = new EditProductCommand()
        {
            Name = "NameTest",
            CategoryId = 1,
            AlcoholContent = ecms.Domain.Enums.AlcoholContentType.Between4AndAHalfAnd18ExceptBeer,
            Description = "Description",
            Id = 1,
            Unit = ecms.Domain.Enums.UnitType.Portion,
            Vat = 8,
            ProductVariants = new List<ProductVariantDto>()
            {
                new ProductVariantDto()
                {
                    Id = 1,
                    ProductId = 1,
                    Name = "Nejm",
                    Price = new ecms.Domain.ValueObjects.Price(22, ecms.Domain.ValueObjects.Currency.Pln)
                },
                new()
                {
                    Id = 0,
                    ProductId = 1,
                    Name = "Test",
                    Price = new ecms.Domain.ValueObjects.Price(22, ecms.Domain.ValueObjects.Currency.Usd)
                }
            }
        };

        ApplicationDbContext.ChangeTracker.Clear();

        //Act
        var result = await Sender.Send(command);

        //Assert
        var editedProduct = ApplicationDbContext.Products.Include(p => p.ProductVariants).FirstOrDefault(p => p.Id == 1);
        editedProduct.Should().NotBeNull();
        editedProduct.Id.Should().Be(1);
        editedProduct.Name.Should().Be(command.Name);
        editedProduct.CategoryId.Should().Be(command.CategoryId);
        editedProduct.AlcoholContent.Should().Be(command.AlcoholContent);
        editedProduct.Description.Should().Be(command.Description);
        editedProduct.Unit.Should().Be(command.Unit);
        editedProduct.Vat.Should().Be(command.Vat);
        editedProduct.ProductVariants.Where(p => p.IsDeleted == false).Should().HaveCount(2);
        editedProduct.ProductVariants.Where(p => p.IsDeleted == false).Select(p => p.Id).Should().NotContain(2);
        editedProduct.ProductVariants.FirstOrDefault(p => p.Id == 1).Name.Should().Be("Nejm");
        editedProduct.ProductVariants.FirstOrDefault(p => p.Id == 1).Price.Amount.Should().Be(22);
        editedProduct.ProductVariants.FirstOrDefault(p => p.Id == 1).Price.Currency.Should().Be(ecms.Domain.ValueObjects.Currency.Pln);
        result.Value.Should().Be(1);
    }
}