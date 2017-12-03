using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Meteo.Api.Controllers;
using Meteo.Core.DTO;
using Meteo.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Meteo.Tests.Controllers
{
    public class WeatherControllerTests
    {
        [Fact]
        public async Task weather_controller_get_should_return_weather_object()
        {
            var fixture = new Fixture();
            var city = "Katowice";
            var weatherDto = fixture.Create<WeatherDto>();
            var weatherServiceMock = new Mock<IWeatherService>();
            weatherServiceMock.Setup(x => x.GetAsync(city)).ReturnsAsync(weatherDto);

            var controller = new WeatherController(weatherServiceMock.Object);

            var result = await controller.Get(city) as OkObjectResult;

            result.Should().NotBeNull();
            var expectedDto = result.Value as WeatherDto;
            weatherDto.ShouldBeEquivalentTo(expectedDto);
            weatherServiceMock.Verify(x => x.GetAsync(city), Times.Once);
        }
    }
}