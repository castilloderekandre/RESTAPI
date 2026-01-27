using RESTAPI.Clients.Interfaces;
using RESTAPI.DTOs;
using RESTAPI.Exceptions;

namespace RESTAPI.Clients
{
    public class OpenWeatherApiClient(HttpClient httpClient) : IOpenWeatherApiClient
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly string _apiKey = Environment.GetEnvironmentVariable("OPENWEATHER_API_KEY")
            ?? throw new InvalidOperationException("OPENWEATHER_API_KEY environment variable is not set");

        public async Task<OpenWeatherDTO?> GetWeatherByCoordinatesAsync(double lat, double lon)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(
                $"weather?lat={lat}&lon={lon}&appid={_apiKey}");

            if (!response.IsSuccessStatusCode)
                throw new ExternalAPIException($"OpenWeather API error ({response.StatusCode}).");

            OpenWeatherDTO? weatherDTO = await response.Content.ReadFromJsonAsync<OpenWeatherDTO>();

            return weatherDTO;
        }
    }
}
