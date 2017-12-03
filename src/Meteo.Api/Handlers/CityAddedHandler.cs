using System.Threading.Tasks;
using Meteo.Core.Events;
using Microsoft.Extensions.Logging;

namespace Meteo.Api.Handlers
{
    public class CityAddedHandler : IEventHandler<CityAdded>
    {
        private readonly ILogger _logger;

        public CityAddedHandler(ILogger<CityAddedHandler> logger)
        {
            _logger = logger;
        }

        public async Task HandleAsync(CityAdded @event)
        {
            _logger.LogInformation($"City added: {@event.Name}");
            await Task.CompletedTask;
        }
    }
}