using api.weatherApi;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace frontend.Data
{
    public class WeatherForecastService
    {
        private readonly ILogger<WeatherForecastService> _logger;
        private readonly IWeatherApiClient _apiClient;

        public WeatherForecastService(ILogger<WeatherForecastService> logger, IWeatherApiClient apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<WeatherForecastResource>> GetForecastAsync(DateTime startDate)
        {
            var forecast = await _apiClient.WeatherForecastAsync();

            return forecast.Select(x => new WeatherForecastResource
            {
                Date = x.Date.LocalDateTime,
                TemperatureC = x.TemperatureC,
                Summary = x.Summary
            });
        }
    }
}
