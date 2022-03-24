using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHueService _hueService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHueService hueService)
        {
            _logger = logger;
            _hueService = hueService;
            _logger.LogInformation("Constructor call");
        }

        [HttpPut("/turnlight9")]
        public Task TurnLight9([FromBody] string turnOn)
        {
            _logger.LogInformation("I was called!");
            return _hueService.TurnLight(turnOn);
        }
        
        [HttpGet("/dodisco")]
        public string DoDisco()
        {
            _logger.LogInformation("Doing disco!");
            _hueService.PerformCoolLightShow();
            return "done";
        }
        
        [HttpGet("/GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }
        
        [HttpGet("/sayhello")]
        public string SayHi()
        {
            return "Hello there!";
        }
        
        [HttpGet("/saysomething")]
        public string SaySomething()
        {
            return "SAYING SOMETHING!";
        }
    }
}