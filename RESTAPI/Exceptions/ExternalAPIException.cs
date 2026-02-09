namespace RESTAPI.Exceptions
{
    public class ExternalAPIException : Exception
    {
        /// <summary>
        /// Represents an exception that is thrown when an error occurs while calling an external API, such as a network error, an invalid response, or an unexpected status code. This exception indicates that the external API is unavailable or returned an error, and it may be used to provide a user-friendly message to the client or to log the error for further investigation.
        /// </summary>
        /// <param name="message">A message that has information or data at the time of when the exception was thrown</param>
        public ExternalAPIException(string message) : base(message)
        {
        }

        /// <summary>
        /// Represents an exception that is thrown when an error occurs while calling an external API, such as a network error, an invalid response, or an unexpected status code. This exception indicates that the external API is unavailable or returned an error, and it may be used to provide a user-friendly message to the client or to log the error for further investigation.
        /// </summary>
        /// <param name="message">A message that has information or data at the time of when the exception was thrown</param>
        /// <param name="innerException">An additional exception which caused this exception to be thrown</param>
        public ExternalAPIException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
