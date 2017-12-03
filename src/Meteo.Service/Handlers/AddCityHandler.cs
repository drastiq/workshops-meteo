using System.Threading.Tasks;
using Meteo.Core.Commands;
using Meteo.Core.Events;
using Meteo.Core.RabbitMq;
using Microsoft.Extensions.Logging;

namespace Meteo.Service.Handlers
{
    public class AddCityHandler : ICommandHandler<AddCity>
    {
        private readonly ILogger _logger;
        private readonly IBusPublisher _busPublisher;

        public AddCityHandler(ILogger<AddCityHandler> logger,
            IBusPublisher busPublisher)
        {
            _logger = logger;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(AddCity command)
        {
            _logger.LogInformation($"Add city: {command.Name}");
            await _busPublisher.PublishEventAsync(new CityAdded { Name = command.Name});
        }
    }
}