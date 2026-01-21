using Microsoft.AspNetCore.Mvc;
using RESTAPI.Clients;
using RESTAPI.Dtos;

namespace RESTAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeocoderController(GeocoderApiClient client) : Controller
    {
        private readonly GeocoderApiClient _client = client;

        [HttpGet("{city}")]
        public async Task<GeocoderDto?> Get(string city)
        {
            return await _client.GetCoordinatesByCityAsync(city);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
