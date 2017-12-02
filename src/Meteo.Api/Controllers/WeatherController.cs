using System.Threading.Tasks;
using Meteo.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Meteo.Api.Controllers
{
    [Route("[controller]")]
    public class WeatherController : Controller
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> Get(string city)
        {
            var result = await _weatherService.GetAsync(city);

            return Ok(result);
        }
    }
}