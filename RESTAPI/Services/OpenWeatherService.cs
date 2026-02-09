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

        /// <summary>
        /// Gets the weather information for the given coordinates (<paramref name="lat"/> and <paramref name="lon"/>).
        /// </summary>
        /// 
        /// <remarks>
        /// This method relies on an external weather API to fetch the weather data based on the provided geographic coordinates.
        /// </remarks>
        /// 
        /// <param name="lat">Latitude in decimal degrees</param>
        /// <param name="lon">Longitude in decimal degrees</param>
        /// 
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains an <see cref="OpenWeatherDTO"/> with the weather details for the given coordinates (<paramref name="lat"/> and <paramref name="lon"/>)
        /// </returns>
        /// 
        /// <exception cref="LocationNotFoundException">
        /// Thrown if the external OpenWeather API did not return any data from the given coordinates
        /// </exception>
        /// <exception cref="WeatherUnavailableException">
        /// Thrown if the external OpenWeather API is unavailable or returns an error
        /// </exception>
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

        /// <summary>
        /// Retrieves the geographic coordinates for the specified city asynchronously.
        /// </summary>
        /// 
        /// <remarks>
        /// This method relies on an external geocoding API.
        /// The returned coordinates may vary depending on the accuracy of the external service.
        /// </remarks>
        /// 
        /// <param name="city">The name of the city for which to obtain geographic coordinates. Cannot be null or empty.</param>
        /// 
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains an <see cref="GeocoderDTO"/> with the coordinates (longitude and latitude) of the specificed city.
        /// </returns>
        /// 
        /// <exception cref="LocationNotFoundException">Thrown if the specified city cannot be found.</exception>
        /// <exception cref="WeatherUnavailableException">Thrown if the external geocoding API is unavailable or returns an error</exception>
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

        /// <summary>
        /// Retrieves the current weather information for the specified city asynchronously.
        /// </summary>
        /// 
        /// <remarks>
        /// This method relies on an external geocoding API to first obtain the geographic coordinates of the city, and then uses those coordinates to fetch weather data from an external weather API.
        /// </remarks>
        /// 
        /// <param name="city">The name of the city for which to obtain weather data. Cannot be null or empty.</param>
        /// 
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an <see cref="OpenWeatherDTO"/>
        /// with the weather details for the specified city.
        /// </returns>
        /// 
        /// <exception cref="LocationNotFoundException">Thrown if the specified city cannot be found or its coordinates cannot be determined.</exception>
        /// <exception cref="WeatherUnavailableException">Thrown if the weather data cannot be retrieved due to an external API error.</exception>
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
