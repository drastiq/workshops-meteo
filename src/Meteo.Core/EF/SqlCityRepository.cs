using System.Collections.Generic;
using System.Threading.Tasks;
using Meteo.Core.Domain;
using Meteo.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Meteo.Core.EF
{
    public class SqlCityRepository : ICityRepository
    {
        private readonly MeteoContext _context;

        public SqlCityRepository(MeteoContext context)
        {
            _context = context;
        }

        public async Task<City> GetAsync(string name)
            => await _context.Cities.SingleOrDefaultAsync(x => x.Name == name);

        public async Task<IEnumerable<City>> BrowseAsync()
            => await _context.Cities.ToListAsync();

        public async Task AddAsync(City city)
        {
            await _context.Cities.AddAsync(city);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string name)
        {
            _context.Cities.Remove(await GetAsync(name));
            await _context.SaveChangesAsync();
        }
    }
}