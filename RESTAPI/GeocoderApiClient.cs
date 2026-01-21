namespace RESTAPI
{
    public class GeocoderApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apikey;

        public GeocoderApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apikey = Environment.GetEnvironmentVariable("OPENWEATHER_API_KEY")
                ?? throw new InvalidOperationException(
                    "OPENWEATHER_API_KEY environment variable is not set.");
        }

        public async Task<string> GetCoordinatesByCityAsync(string city)
        {
            var response = await _httpClient.GetAsync(
                $"direct?q={Uri.EscapeDataString(city)}&limit=1&appid={_apikey}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
