using api.todoApi;
using frontend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace frontend.Data
{
    public class TodoService
    {
        private readonly ILogger<TodoService> _logger;

        public TodoService(ILogger<TodoService> logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<TodoItemResource>> Get()
        {
            _logger.LogInformation("In front-end");
            var httpClient = new HttpClient();
            var client = new api.todoApi.TodoApiClient("https://localhost:5005", httpClient);
            var todoItems = await client.TodoItemsAllAsync();

            return todoItems.Select(x => new TodoItemResource
            {
                Id = x.Id,
                IsComplete = x.IsComplete,
                Name = x.Name,
                WeatherForecast = new WeatherForecastResource
                {
                    Date = (x.WeatherForecast?.Date ?? DateTimeOffset.MinValue).LocalDateTime,
                    Summary = x.WeatherForecast?.Summary,
                    TemperatureC = x.WeatherForecast?.TemperatureC ?? 0,
                }
            })
            .ToArray();
        }

        public async Task<TodoItemResource> Save([FromBody]TodoItemResource todoItem)
        {
            _logger.LogInformation("In front-end");
            var httpClient = new HttpClient();
            var client = new api.todoApi.TodoApiClient("https://localhost:5005", httpClient);

            var toSave = new TodoItem
            {
                Name = todoItem.Name
            };
            var result = await client.TodoItemsAsync(toSave);

            return new TodoItemResource
            {
                Id = result.Id,
                IsComplete = result.IsComplete,
                Name = result.Name
            };
        }
    }
}
