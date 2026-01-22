using Microsoft.AspNetCore.Mvc;
using RESTAPI.Clients;
using RESTAPI.Services;

namespace RESTAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpenWeatherController(OpenWeatherService openWeatherService) : Controller
    {
        //29.7589382
        //-95.3676974
        private readonly OpenWeatherService _openWeatherService = openWeatherService;

        [HttpGet("{city}")]
        public async Task<IActionResult> Get(string city)
        {
            var weatherDto = await _openWeatherService.GetWeatherByCityAsync(city);
            if (weatherDto == null)
            {
                return NotFound();
            }
            return Ok(weatherDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetByCoordinates(
            [FromQuery] double lat,
            [FromQuery] double lon)
        {
            var weatherDto = await _openWeatherService.GetWeatherByCoordinatesAsync(lat, lon);
            if (weatherDto == null)
            {
                return NotFound();
            }
            return Ok(weatherDto);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
