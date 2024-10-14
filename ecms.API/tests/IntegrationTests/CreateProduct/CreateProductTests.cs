using ecms.Application.Handlers.Commands.CreateProduct;
using ecms.Application.Models.Dtos.Products;
using ecms.Domain.Entities;
using FluentAssertions;
using IntegrationTests.Abstractions;

namespace IntegrationTests.CreateProduct;

public class CreateProductTests : BaseIntegrationTest
{
    public CreateProductTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
        Seed();
    }

    private void Seed()
    {
        var category = new CategoryEntity
        {
            HierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId(),
            Name = "TestCategory1",
        };

        ApplicationDbContext.Categories.Add(category);
        ApplicationDbContext.SaveChanges();
    }

    [Fact]
    public async Task CreateProductCommand_ShouldCreateProduct_OnValidRequest()
    {
        //Arrange
        var command = new CreateProductCommand()
        {
            Name = "name",
            Description = "description",
            CategoryId = 1,
            AlcoholContent = ecms.Domain.Enums.AlcoholContentType.AlcoholFree,
            Vat = 23,
            Unit = ecms.Domain.Enums.UnitType.Piece,
            ProductVariants = new List<CreateProductVariantDto>()
            {
                new CreateProductVariantDto()
                {
                    Name = "name",
                    Price = new ecms.Domain.ValueObjects.Price(32, ecms.Domain.ValueObjects.Currency.Pln)
                }
            }
        };

        //Act
        var result = await Sender.Send(command);

        //Assert
        result.Value.Should().Be(1);
    }
}