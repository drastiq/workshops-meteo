using System.Threading.Tasks;
using FluentAssertions;
using Meteo.Core.DTO;
using Xunit;

namespace Meteo.Tests.EndToEnd.Controllers
{
    public class WeatherControllerTests : TestControllerBase
    {
        [Fact]
        public async Task weather_controller_get_should_return_weather_object()
        {
            var city = "krakow";
            var weatherDto = await GetAsync<WeatherDto>($"weather/{city}");
            weatherDto.Should().NotBeNull();
            weatherDto.City.ShouldBeEquivalentTo(city);
        }        
    }
}