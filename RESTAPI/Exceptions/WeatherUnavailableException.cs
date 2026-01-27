namespace RESTAPI.Exceptions
{
    public class WeatherUnavailableException : Exception
    {
        public WeatherUnavailableException(string message) : base(message)
        {
        }

        public WeatherUnavailableException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
