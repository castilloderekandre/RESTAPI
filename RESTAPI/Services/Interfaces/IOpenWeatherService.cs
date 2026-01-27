using RESTAPI.DTOs;

namespace RESTAPI.Services.Interfaces
{
    public interface IOpenWeatherService
    {
        Task<OpenWeatherDTO> GetWeatherByCoordinatesAsync(double lat, double lon);
        Task<OpenWeatherDTO> GetWeatherByCityAsync(string city);
        Task<GeocoderDTO> GetCoordinatesByCityAsync(string city);
    }
}
