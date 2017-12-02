using Meteo.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Meteo.Api.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Get()
            => Content($"Hello from Meteo API.");

        [HttpGet("error")]
        public IActionResult Error()
            => Content("There was an error.");
    }
}