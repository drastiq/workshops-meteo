using System.Threading.Tasks;
using Meteo.Core.Commands;
using Meteo.Core.Events;

namespace Meteo.Core.RabbitMq
{
    public interface IBusPublisher
    {
        Task PublishCommandAsync<T>(T command) where T : ICommand;
        Task PublishEventAsync<T>(T @event) where T : IEvent; 
    }
}