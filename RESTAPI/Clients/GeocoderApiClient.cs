using RESTAPI.DTOs;

namespace RESTAPI.Clients
{
    public class GeocoderApiClient(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly string _apikey = Environment.GetEnvironmentVariable("OPENWEATHER_API_KEY")
                ?? throw new InvalidOperationException(
                    "OPENWEATHER_API_KEY environment variable is not set.");

        public async Task<GeocoderDTO?> GetCoordinatesByCityAsync(string city)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(
                $"direct?q={Uri.EscapeDataString(city)}&limit=1&appid={_apikey}");

            if(!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Geocoding API error ({response.StatusCode})");

            List<GeocoderDTO>? geocoderDTO = await response.Content.ReadFromJsonAsync<List<GeocoderDTO>>();

            return geocoderDTO?.FirstOrDefault();
        }
    }
}
