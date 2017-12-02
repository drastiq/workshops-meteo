using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Meteo.Core.Domain;
using Meteo.Core.DTO;
using Meteo.Core.Repositories;

namespace Meteo.Core.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CityService(ICityRepository cityRepository,
            IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(string name)
        {
            var city = await _cityRepository.GetAsync(name);
            if (city != null)
            {
                throw new Exception($"City {city} already exists.");
            }
            city = new City(name);
            await _cityRepository.AddAsync(city);
        }

        public async Task<IEnumerable<CityDto>> BrowseAsync()
            => _mapper.Map<IEnumerable<CityDto>>(await _cityRepository.BrowseAsync());

        public async Task DeleteAsync(string name)
            => await _cityRepository.DeleteAsync(name);
    }
}