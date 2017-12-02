using System.Threading.Tasks;
using Meteo.Core.DTO;

namespace Meteo.Core.Services
{
    public interface IWeatherService
    {
        Task<WeatherDto> GetAsync(string city);
    }
}