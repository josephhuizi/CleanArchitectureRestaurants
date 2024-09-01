using Restaurants.API;

public interface IWeatherForecastService
{
    IEnumerable<WeatherForecast> Get(int resultsNumber, int maximumTemperature, int minimumTemperature);
}

public class WeatherForecastService : IWeatherForecastService
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    public IEnumerable<WeatherForecast> Get(int resultsNumber, int maximumTemperature, int minimumTemperature)
    {
        return Enumerable.Range(1, resultsNumber).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(minimumTemperature, maximumTemperature),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}

