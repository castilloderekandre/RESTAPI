namespace RESTAPI.DTOs
{
    public class GeocoderDTO
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
    }
}
