using System.Threading.Tasks;

namespace Meteo.Core.Commands
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}