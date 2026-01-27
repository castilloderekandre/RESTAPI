using RESTAPI.Clients;
using RESTAPI.Clients.Interfaces;
using RESTAPI.DTOs;
using RESTAPI.Exceptions;
using RESTAPI.Services.Interfaces;

namespace RESTAPI.Services
{
    public class OpenWeatherService(
        IOpenWeatherApiClient openWeatherApiClient,
        IGeocoderApiClient geocoderApiClient) : IOpenWeatherService
    {
        private readonly IOpenWeatherApiClient _openWeatherApiClient = openWeatherApiClient;
        private readonly IGeocoderApiClient _geocoderApiClient = geocoderApiClient;

        public async Task<OpenWeatherDTO> GetWeatherByCoordinatesAsync(double lat, double lon)
        {
            try
            {
                return await _openWeatherApiClient.GetWeatherByCoordinatesAsync(lat, lon)
                    ?? throw new LocationNotFoundException($"{lat}, {lon}");
            }
            catch (ExternalAPIException ex)
            {
                throw new WeatherUnavailableException($"{lat}, {lon}", ex);
            }
        }

        public async Task<GeocoderDTO> GetCoordinatesByCityAsync(string city)
        {
            try
            {
                return await _geocoderApiClient.GetCoordinatesByCityAsync(city)
                    ?? throw new LocationNotFoundException(city);
            }
            catch (ExternalAPIException ex)
            {
                throw new WeatherUnavailableException(city, ex);
            }
        }

        public async Task<OpenWeatherDTO> GetWeatherByCityAsync(string city)
        {
            GeocoderDTO geocoderDTO = await GetCoordinatesByCityAsync(city);
            
            try
            {
                return await _openWeatherApiClient.GetWeatherByCoordinatesAsync(geocoderDTO.Lat, geocoderDTO.Lon)
                    ?? throw new LocationNotFoundException(city);
            }
            catch (ExternalAPIException ex)
            {
                throw new WeatherUnavailableException(city, ex);
            }
        }
    }
}
