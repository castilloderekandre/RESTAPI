using RESTAPI.Clients.Interfaces;
using RESTAPI.DTOs;
using RESTAPI.Exceptions;

namespace RESTAPI.Clients
{
    public class GeocoderApiClient(HttpClient httpClient) : IGeocoderApiClient
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly string _apikey = Environment.GetEnvironmentVariable("OPENWEATHER_API_KEY")
            ?? throw new InvalidOperationException("OPENWEATHER_API_KEY environment variable is not set.");

        public async Task<GeocoderDTO?> GetCoordinatesByCityAsync(string city)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(
                $"direct?q={Uri.EscapeDataString(city)}&limit=1&appid={_apikey}");

            if (!response.IsSuccessStatusCode)
                throw new ExternalAPIException($"Geocoding API error ({response.StatusCode})");

            List<GeocoderDTO>? geocoderDTO = await response.Content.ReadFromJsonAsync<List<GeocoderDTO>>();

            return geocoderDTO?.FirstOrDefault();
        }
    }
}
