using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meteo.Core.DTO;
using Meteo.Core.Services;

namespace Meteo.Tests.EndToEnd.Services
{
    public class CityService : ICityService
    {
        public async Task AddAsync(string name)
        {
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<CityDto>> BrowseAsync()
        {
            return await Task.FromResult(Enumerable.Empty<CityDto>());
        }

        public async Task DeleteAsync(string name)
        {
            await Task.CompletedTask;
        }
    }
}