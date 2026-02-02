using Microsoft.AspNetCore.Mvc;
using RESTAPI.Clients;
using RESTAPI.Clients.Interfaces;
using RESTAPI.DTOs;
using RESTAPI.Exceptions;
using RESTAPI.Services.Interfaces;

namespace RESTAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeocoderController(IOpenWeatherService openWeatherService) : Controller
    {
        private readonly IOpenWeatherService _openWeatherService = openWeatherService;

        [HttpGet("city/{city}")]
        public async Task<IActionResult> Get(string city)
        {
            if (string.IsNullOrEmpty(city))
                return BadRequest("City name cannot be null or empty.");

            var geocoderDto = await _openWeatherService.GetCoordinatesByCityAsync(city);
            return Ok(geocoderDto);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
