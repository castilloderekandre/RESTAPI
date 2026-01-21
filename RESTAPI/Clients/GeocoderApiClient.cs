using RESTAPI.Dtos;

namespace RESTAPI.Clients
{
    public class GeocoderApiClient(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly string _apikey = Environment.GetEnvironmentVariable("OPENWEATHER_API_KEY")
                ?? throw new InvalidOperationException(
                    "OPENWEATHER_API_KEY environment variable is not set.");

        public async Task<GeocoderDto?> GetCoordinatesByCityAsync(string city)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(
                $"direct?q={Uri.EscapeDataString(city)}&limit=1&appid={_apikey}");

            response.EnsureSuccessStatusCode();

            List<GeocoderDto>? geocoderDto = await response.Content.ReadFromJsonAsync<List<GeocoderDto>>();

            return geocoderDto?.FirstOrDefault();
        }
    }
}
