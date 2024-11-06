using ecms.Application.Handlers.Queries.GetProductDetailsById;
using ecms.Application.Models.ViewModels.Products;
using ecms.Domain.Entities;
using FluentAssertions;
using IntegrationTests.Abstractions;
using SharedKernel;

namespace IntegrationTests.GetProductDetailsById;

public class GetProductDetailsByIdTests : BaseIntegrationTest
{
    public GetProductDetailsByIdTests(IntegrationTestWebAppFactory factory) : base(factory)
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

        ApplicationDbContext.Products.AddRange(products);
        ApplicationDbContext.SaveChanges();
    }

    [Fact]
    public async Task GetProductDetailsById_ShouldReturnSuccessResult_OnValidRequest()
    {
        //Arrange
        var query = new GetProductDetailsByIdQuery(3);

        //Act
        var result = await Sender.Send(query);

        //Assert
        result.Should().NotBeNull();
        result.Value.ProductId.Should().Be(3);
        result.Value.Name.Should().Be("Test3");
        result.Value.Description.Should().Be("Test Description");
        result.Value.AlcoholContent.Should().Be(ecms.Domain.Enums.AlcoholContentType.UpTo4AndAHalfPercentOrBeer);
        result.Value.Vat.Should().Be(23);
        result.Value.Unit.Should().Be(ecms.Domain.Enums.UnitType.Weight);
        result.Value.CategoryId.Should().Be(1);
        result.Value.productVariantDtos.Should().BeEmpty();
        result.Should().BeOfType<Result<ProductDetailsViewModel>>();
        result.IsSuccess.Should().BeTrue();
    }
}