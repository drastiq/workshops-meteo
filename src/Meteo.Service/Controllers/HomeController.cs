using Microsoft.AspNetCore.Mvc;

namespace Meteo.Service.Controllers
{    
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Get()
            => Content($"Hello from Meteo Service.");
    }
}