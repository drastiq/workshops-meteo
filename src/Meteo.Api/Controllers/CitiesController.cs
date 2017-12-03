using System.Threading.Tasks;
using Meteo.Core.Commands;
using Meteo.Core.RabbitMq;
using Meteo.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Meteo.Api.Controllers
{
    [Route("[controller]")]
    public class CitiesController : Controller
    {
        private readonly ICityService _cityService;
        private readonly IBusPublisher _busPublisher;

        public CitiesController(ICityService cityService,
            IBusPublisher busPublisher)
        {
            _cityService = cityService;
            _busPublisher = busPublisher;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _cityService.BrowseAsync());

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddCity command)
        {
            // await _cityService.AddAsync(command.Name);
            await _busPublisher.PublishCommandAsync(command);

            return Created("cities", null);
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            await _cityService.DeleteAsync(name);

            return NoContent();
        }
    }
}