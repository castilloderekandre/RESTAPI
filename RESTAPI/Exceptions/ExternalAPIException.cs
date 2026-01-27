namespace RESTAPI.Exceptions
{
    public class ExternalAPIException : Exception
    {
        public ExternalAPIException(string message) : base(message)
        {
        }

        public ExternalAPIException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
