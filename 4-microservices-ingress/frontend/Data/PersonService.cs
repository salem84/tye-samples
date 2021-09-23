using api.personApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace frontend.Data
{
    public class PersonService
    {
        private readonly ILogger<PersonService> _logger;
        private readonly IPersonApiClient _apiClient;

        public PersonService(ILogger<PersonService> logger, IPersonApiClient apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<PersonResource>> Get()
        {
            _logger.LogInformation("In front-end");
            var people = await _apiClient.PersonAllAsync();

            return people.Select(x => new PersonResource
            {
                EmailAddress = x.EmailAddress,
                FirstName = x.FirstName,
                Id = x.Id,
                LastName = x.LastName,
                Password = x.Password,
                TodoItems = x?.TodoItems?.Select(t => new TodoItemResource
                {
                    Id = t.Id,
                    IsComplete = t.IsComplete,
                    Name = t.Name,
                    WeatherForecast = new WeatherForecastResource
                    {
                        Date = t.WeatherForecast.Date.LocalDateTime,
                        Summary = t.WeatherForecast.Summary,
                        TemperatureC = t.WeatherForecast.TemperatureC,
                    }
                })

            })
            .ToArray();
        }

        public async Task<PersonResource> Save(PersonResource person)
        {
            _logger.LogInformation("In front-end");

            var toSave = new Person
            {
                EmailAddress = person.EmailAddress,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Password= person.Password
            };
            var result = await _apiClient.PersonAsync(toSave);

            return new PersonResource
            {
                EmailAddress = result.EmailAddress,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Password = result.Password
            };
        }
    }
}
