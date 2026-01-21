using RESTAPI.Dtos;

namespace RESTAPI.Clients
{
    public class OpenWeatherApiClient(HttpClient httpClient, GeocoderApiClient geocoderClient)
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly GeocoderApiClient _geocoderApiClient = geocoderClient;
        private readonly string _apiKey = Environment.GetEnvironmentVariable("OPENWEATHER_API_KEY") ?? 
            throw new InvalidOperationException("OPENWEATHER_API_KEY was not set");

        public async Task<OpenWeatherDto?> GetWeatherByCoordinatesAsync(double lat, double lon)
        {
            Console.WriteLine($"Fetching weather for coordinates: {lat}, {lon}");
            HttpResponseMessage response = await _httpClient.GetAsync(
                $"weather?lat={lat}&lon={lon}&appid={_apiKey}");

            response.EnsureSuccessStatusCode();

            OpenWeatherDto? weatherDto = await response.Content.ReadFromJsonAsync<OpenWeatherDto>();

            return weatherDto;
        }

        public async Task<OpenWeatherDto?> GetWeatherByCityAsync(string city)
        {
            GeocoderDto? geocoderDto = await _geocoderApiClient.GetCoordinatesByCityAsync(city)
                ?? throw new InvalidOperationException($"Could not find coordinates for city: {city}");

            OpenWeatherDto? weatherDto = await GetWeatherByCoordinatesAsync(geocoderDto.Lat, geocoderDto.Lon);

            return weatherDto;
        }
    }
}
