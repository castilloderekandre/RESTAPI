using Microsoft.AspNetCore.Mvc;
using RESTAPI.Clients;
using RESTAPI.DTOs;

namespace RESTAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeocoderController(GeocoderApiClient geocoderApiClient) : Controller
    {
        private readonly GeocoderApiClient _geocoderApiClient = geocoderApiClient;

        [HttpGet("{city}")]
        public async Task<IActionResult> Get(string city)
        {
            if (string.IsNullOrEmpty(city))
                return BadRequest("City name cannot be null or empty.");

            try
            {
                var geocoderDto = await _geocoderApiClient.GetCoordinatesByCityAsync(city);
                return Ok(geocoderDto);
            }
            catch (Exception)
            {
                return NotFound($"Coordinates for city '{city}' not found.");
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
