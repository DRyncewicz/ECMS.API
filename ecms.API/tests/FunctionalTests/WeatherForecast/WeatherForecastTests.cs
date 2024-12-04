using FluentAssertions;
using FunctionalTests.Abstractions;
using System.Net;

namespace FunctionalTests.WeatherForecast;

public class WeatherForecastTests : BaseFunctionalTest
{
    public WeatherForecastTests(FunctionalTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Get_ShouldReturnRandomForecasts()
    {
        //Arrange

        //Act
        var response = await AuthorizedHttpClient.GetAsync("api/v1/WeatherForecast");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}