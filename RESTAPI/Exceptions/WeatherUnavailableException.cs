namespace RESTAPI.Exceptions
{
    public class WeatherUnavailableException : Exception
    {
        /// <summary>
        /// Represents an exception that is thrown when any step of the process of retrieving weather information fails, such as when an external API is unavailable or returns an error. This exception indicates that the weather information cannot be retrieved at the moment, and it may be used to provide a user-friendly message to the client or to log the error for further investigation.
        /// </summary>
        /// <param name="message">A message that has information or data at the time of when the exception was thrown</param>
        public WeatherUnavailableException(string message) : base(message)
        {
        }

        /// <summary>
        /// Represents an exception that is thrown when any step of the process of retrieving weather information fails, such as when an external API is unavailable or returns an error. This exception indicates that the weather information cannot be retrieved at the moment, and it may be used to provide a user-friendly message to the client or to log the error for further investigation.
        /// </summary>
        /// <param name="message">A message that has information or data at the time of when the exception was thrown</param>
        /// <param name="innerException">An additional exception which caused this exception to be thrown</param>
        public WeatherUnavailableException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
