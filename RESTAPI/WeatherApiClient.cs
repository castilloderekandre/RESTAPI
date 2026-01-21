namespace RESTAPI
{
    public class WeatherApiClient
    {
        private readonly HttpClient _httpClient;
        public WeatherApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetWeatherAsync(string city)
        {
            var response = await _httpClient.GetAsync(
                $"weather?city={Uri.EscapeDataString(city)}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
