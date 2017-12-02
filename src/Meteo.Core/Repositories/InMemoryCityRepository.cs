using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meteo.Core.Domain;

namespace Meteo.Core.Repositories
{
    public class InMemoryCityRepository : ICityRepository
    {
        private readonly ISet<City> _cities = new HashSet<City>();

        public async Task<City> GetAsync(string name)
            => await Task.FromResult(_cities.SingleOrDefault(x => x.Name == name.ToLowerInvariant()));

        public async Task<IEnumerable<City>> BrowseAsync()
            => await Task.FromResult(_cities);

        public async Task AddAsync(City city)
        {
            _cities.Add(city);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(string name)
            => _cities.Remove(await GetAsync(name));
    }
}