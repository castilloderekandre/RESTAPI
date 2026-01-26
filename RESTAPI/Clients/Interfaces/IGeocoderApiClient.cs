using RESTAPI.DTOs;

namespace RESTAPI.Clients.Interfaces
{
    public interface IGeocoderApiClient
    {
        Task<GeocoderDTO?> GetCoordinatesByCityAsync(string city);
    }
}
