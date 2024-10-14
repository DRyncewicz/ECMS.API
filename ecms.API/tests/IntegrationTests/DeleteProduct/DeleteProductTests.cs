using ecms.Application.Handlers.Commands.DeleteProduct;
using ecms.Domain.Entities;
using FluentAssertions;
using IntegrationTests.Abstractions;

namespace IntegrationTests.DeleteProduct;

public class DeleteProductTests : BaseIntegrationTest
{
    public DeleteProductTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
        Seed();
    }

    private void Seed()
    {
        var products = new List<ProductEntity>
        {
             new ProductEntity {Name = "Test1", Description = "Test Description", AlcoholContent = ecms.Domain.Enums.AlcoholContentType.UpTo4AndAHalfPercentOrBeer, Vat = 23, Unit = ecms.Domain.Enums.UnitType.Weight, UserId = "userId", CategoryId = 1 },
             new ProductEntity {Name = "Test2", Description = "Test Description", AlcoholContent = ecms.Domain.Enums.AlcoholContentType.UpTo4AndAHalfPercentOrBeer, Vat = 23, Unit = ecms.Domain.Enums.UnitType.Weight, UserId = "userId", CategoryId = 1 },
             new ProductEntity {Name = "Test3", Description = "Test Description", AlcoholContent = ecms.Domain.Enums.AlcoholContentType.UpTo4AndAHalfPercentOrBeer, Vat = 23, Unit = ecms.Domain.Enums.UnitType.Weight, UserId = "userId", CategoryId = 1 }
        };

        var category = new CategoryEntity
        {
            HierarchyId = new Microsoft.EntityFrameworkCore.HierarchyId(),
            Name = "TestCategory1",
        };

        ApplicationDbContext.Categories.Add(category);
        ApplicationDbContext.SaveChanges();

        ApplicationDbContext.Products.AddRange(products);
        ApplicationDbContext.SaveChanges();
    }

    [Fact]
    public async Task DeleteProductCommand_ShouldDeleteProduct_OnValidRequest()
    {
        //Arrange
        var command = new DeleteProductCommand(1);

        //Act
        var result = await Sender.Send(command);

        //Assert
        result.Value.Should().Be(true);
        ApplicationDbContext.Products.FirstOrDefault(p => p.Id == 1).IsDeleted.Should().Be(true);
    }
}