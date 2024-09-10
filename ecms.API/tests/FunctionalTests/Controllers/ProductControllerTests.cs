using ecms.Application.Models.ViewModels.Products;
using FluentAssertions;
using FunctionalTests.Abstractions;
using SharedKernel;
using System.Net;

namespace FunctionalTests.Controllers;

public class ProductControllerTests : BaseFunctionalTest
{
    public ProductControllerTests(FunctionalTestWebAppFactory factory) : base(factory)
    {

    }

    [Fact]
    public async Task GetProducts_UnauthorisedClientShouldGetUnauthorizedResponse()
    {
        //Act
        var response = await HttpClient.GetAsync("api/v1/Product/by-filters");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
