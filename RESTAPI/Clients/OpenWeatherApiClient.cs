using RESTAPI.DTOs;

namespace RESTAPI.Clients
{
    public class OpenWeatherApiClient(HttpClient httpClient, GeocoderApiClient geocoderClient)
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly string _apiKey = Environment.GetEnvironmentVariable("OPENWEATHER_API_KEY") ?? 
            throw new InvalidOperationException("OPENWEATHER_API_KEY was not set");

        public async Task<OpenWeatherDTO?> GetWeatherByCoordinatesAsync(double lat, double lon)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(
                $"weather?lat={lat}&lon={lon}&appid={_apiKey}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                throw new Exception("City not found.");

            if (!response.IsSuccessStatusCode)
                throw new Exception($"OpenWeather API error ({response.StatusCode}).");

            OpenWeatherDTO? weatherDTO = await response.Content.ReadFromJsonAsync<OpenWeatherDTO>();

            return weatherDTO;
        }
    }
}
