using RESTAPI.Clients;
using RESTAPI.Dtos;

namespace RESTAPI.Services
{
    public class OpenWeatherService(OpenWeatherApiClient openWeatherApiClient,
        GeocoderApiClient geocoderApiClient)
    {
        private readonly OpenWeatherApiClient _openWeatherApiClient = openWeatherApiClient;
        private readonly GeocoderApiClient _geocoderApiClient = geocoderApiClient;

        public async Task<OpenWeatherDto?> GetWeatherByCoordinatesAsync(double lat, double lon)
        {
            OpenWeatherDto? weatherDto = await _openWeatherApiClient.GetWeatherByCoordinatesAsync(lat, lon);
            return weatherDto;
        }

        public async Task<OpenWeatherDto?> GetWeatherByCityAsync(string city)
        {
            GeocoderDto? geocoderDto = await _geocoderApiClient.GetCoordinatesByCityAsync(city)
                ?? throw new InvalidOperationException($"Could not find coordinates for city: {city}");

            OpenWeatherDto? weatherDto = await _openWeatherApiClient.GetWeatherByCoordinatesAsync(geocoderDto.Lat, geocoderDto.Lon);

            return weatherDto;
        }
    }
}
