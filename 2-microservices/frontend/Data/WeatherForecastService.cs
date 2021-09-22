using api.weatherApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace frontend.Data
{
    public class WeatherForecastService
    {
        public async Task<IEnumerable<WeatherForecastResource>> GetForecastAsync(DateTime startDate)
        {
            var httpClient = new HttpClient();
            var client = new WeatherApiClient("https://localhost:5003", httpClient);
            var forecast = await client.WeatherForecastAsync();

            return forecast.Select(x => new WeatherForecastResource
            {
                Date = x.Date.LocalDateTime,
                TemperatureC = x.TemperatureC,
                Summary = x.Summary
            });
        }
    }
}
