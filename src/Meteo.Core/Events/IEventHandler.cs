using System.Threading.Tasks;

namespace Meteo.Core.Events
{
    public interface IEventHandler<T> where T : IEvent
    {
        Task HandleAsync(T @event);
    }
}