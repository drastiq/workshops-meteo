using System.Collections.Generic;
using System.Threading.Tasks;
using Meteo.Core.DTO;

namespace Meteo.Core.Services
{
    public interface ICityService
    {
        Task AddAsync(string name);
        Task<IEnumerable<CityDto>> BrowseAsync();
        Task DeleteAsync(string name);
    }
}