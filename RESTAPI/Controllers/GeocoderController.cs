using Microsoft.AspNetCore.Mvc;

namespace RESTAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeocoderController : Controller
    {
        private readonly GeocoderApiClient _client;
        public GeocoderController(GeocoderApiClient client)
        {
            _client = client;
        }

        [HttpGet("{city}")]
        public async Task<string> Get(string city)
        {
            return await _client.GetCoordinatesByCityAsync(city);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
