using Microsoft.AspNetCore.Mvc;
using RESTAPI.Clients;
using RESTAPI.Services;
using RESTAPI.Services.Interfaces;

namespace RESTAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpenWeatherController(IOpenWeatherService openWeatherService) : Controller
    {
        //29.7589382
        //-95.3676974
        private readonly IOpenWeatherService _openWeatherService = openWeatherService;

        [HttpGet("city/{city}")]
        public async Task<IActionResult> GetByCity(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                return BadRequest("City name cannot be empty.");
            try
            {
                var weatherDto = await _openWeatherService.GetWeatherByCityAsync(city);
                return Ok(weatherDto);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetByCoordinates(
            [FromQuery] double lat,
            [FromQuery] double lon)
        {
            if (lat < -90 || lat > 90 || lon < -180 || lon > 180)
                return BadRequest("Invalid latitude or longitude values.");
            try
            {
                var weatherDto = await _openWeatherService.GetWeatherByCoordinatesAsync(lat, lon);
                return Ok(weatherDto);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
