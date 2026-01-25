using RESTAPI.Clients;
using RESTAPI.DTOs;

namespace RESTAPI.Services
{
    public class OpenWeatherService(OpenWeatherApiClient openWeatherApiClient,
        GeocoderApiClient geocoderApiClient)
    {
        private readonly OpenWeatherApiClient _openWeatherApiClient = openWeatherApiClient;
        private readonly GeocoderApiClient _geocoderApiClient = geocoderApiClient;

        public async Task<OpenWeatherDTO?> GetWeatherByCoordinatesAsync(double lat, double lon)
        {
            OpenWeatherDTO? weatherDTO = await _openWeatherApiClient.GetWeatherByCoordinatesAsync(lat, lon);
            return weatherDTO;
        }

        public async Task<OpenWeatherDTO?> GetWeatherByCityAsync(string city)
        {
            GeocoderDTO? geocoderDTO = await _geocoderApiClient.GetCoordinatesByCityAsync(city)
                ?? throw new InvalidOperationException($"Could not find coordinates for city: {city}");

            OpenWeatherDTO? weatherDTO = await _openWeatherApiClient.GetWeatherByCoordinatesAsync(geocoderDTO.Lat, geocoderDTO.Lon);

            return weatherDTO;
        }
    }
}
