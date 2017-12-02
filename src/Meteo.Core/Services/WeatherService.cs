using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Meteo.Core.DTO;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Meteo.Core.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly string _apiKey;
        private readonly ILogger _logger;
        private static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://api.weatherbit.io/v2.0/")
        };
        private readonly IMemoryCache _cache;

        public WeatherService(IOptions<WeatherServiceOptions> options,
            ILogger<WeatherService> logger,
            IMemoryCache cache)
        {
            _apiKey = options.Value.ApiKey;
            _logger = logger;
            _cache = cache;
        }

        public async Task<WeatherDto> GetAsync(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentException("City name can not be empty.", nameof(city));
            }
            var dto = _cache.Get<WeatherDto>($"weather:{city}");
            if (dto != null)
            {
                _logger.LogInformation($"Weather for: {city} was found in cache.");

                return dto;
            }
            _logger.LogInformation($"Get weather for city: {city}");
            var response = await _httpClient.GetAsync($"current?city={city}&key={_apiKey}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result>(content);
            dto = new WeatherDto
            {
                City = result.Data.First().City,
                Temperature = result.Data.First().Temperature
            };
            // dto = new WeatherDto
            // {
            //     City = city,
            //     Temperature = 2
            // };
            _cache.Set($"weather:{city}", dto, TimeSpan.FromMinutes(5));

            return dto;
        }

        private class Result
        {
            public IEnumerable<Data> Data { get; set; }
        }

        private class Data
        {
            [JsonProperty("city_name")]
            public string City { get; set; }

            [JsonProperty("temp")]
            public double Temperature { get; set; }
        }
    }
}