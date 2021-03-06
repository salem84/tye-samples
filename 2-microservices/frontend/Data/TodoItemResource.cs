using System.ComponentModel.DataAnnotations;

namespace frontend.Data
{
    public class TodoItemResource
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsComplete { get; set; }

        public WeatherForecastResource WeatherForecast { get; set; }
    }
}
