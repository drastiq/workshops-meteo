using System.Threading.Tasks;
using Meteo.Core.DTO;
using Meteo.Core.Services;

namespace Meteo.Tests.EndToEnd.Services
{
    public class WeatherService : IWeatherService
    {
        public async Task<WeatherDto> GetAsync(string city)
            => await Task.FromResult(new WeatherDto
            {
                City = city,
                Temperature = 1
            });
    }
}