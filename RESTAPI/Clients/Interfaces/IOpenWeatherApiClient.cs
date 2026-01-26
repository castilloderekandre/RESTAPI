using RESTAPI.DTOs;

namespace RESTAPI.Clients.Interfaces
{
    public interface IOpenWeatherApiClient
    {
        Task<OpenWeatherDTO?> GetWeatherByCoordinatesAsync(double lat, double lon);
    }
}
