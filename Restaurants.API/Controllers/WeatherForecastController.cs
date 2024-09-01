using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherForecastService _weatherForecastService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService)
    {
        _logger = logger;
        _weatherForecastService = weatherForecastService;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        var result = _weatherForecastService.Get(5, 20, -27);
        return result;
    } 

    [HttpPost]
    [Route("generate")]
    public ActionResult<WeatherForecast> Test([FromQuery] int resultsNumber, [FromBody] TemperatureParameters parameters)
    {
        if ( resultsNumber < 1 || (parameters.MaximumTemperature <= parameters.MinimumTemperature) )
        {
            return BadRequest();
        }
        
        var result = _weatherForecastService.Get(resultsNumber, parameters.MaximumTemperature, parameters.MinimumTemperature);
        return StatusCode(200, result);
    }
}


