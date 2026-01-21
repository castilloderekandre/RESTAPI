using Microsoft.AspNetCore.Mvc;
using RESTAPI.Clients;

namespace RESTAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpenWeatherController(OpenWeatherApiClient openWeatherApiClient) : Controller
    {
        //29.7589382
        //-95.3676974
        private readonly OpenWeatherApiClient _openWeatherApiClient = openWeatherApiClient;

        [HttpGet("{city}")]
        public async Task<IActionResult> Get(string city)
        {
            var weatherDto = await _openWeatherApiClient.GetWeatherByCityAsync(city);
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
            var weatherDto = await _openWeatherApiClient.GetWeatherByCoordinatesAsync(lat, lon);
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
