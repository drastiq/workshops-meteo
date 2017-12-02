using AutoMapper;
using Meteo.Core.Domain;
using Meteo.Core.DTO;

namespace Meteo.Core.Mapper
{
    public static class AutoMapperConfig
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<City,CityDto>()
                    .ForMember(m => m.Title, x => x.MapFrom(p => p.Name));
            });

            return config.CreateMapper();
        }
    }
}