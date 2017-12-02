using Meteo.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Meteo.Api.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly IOptions<WeatherServiceOptions> _options;

        public HomeController(IOptions<WeatherServiceOptions> options)
        {
            _options = options;
        }

        [HttpGet]
        public IActionResult Get()
            => Content($"Hello {_options.Value.ApiKey}");
    }
}