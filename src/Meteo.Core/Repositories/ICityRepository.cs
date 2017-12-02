using System.Collections.Generic;
using System.Threading.Tasks;
using Meteo.Core.Domain;

namespace Meteo.Core.Repositories
{
    public interface ICityRepository
    {
        Task<City> GetAsync(string name);
        Task<IEnumerable<City>> BrowseAsync();
        Task AddAsync(City city);
        Task DeleteAsync(string name);
    }
}