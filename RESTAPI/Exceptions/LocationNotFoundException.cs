namespace RESTAPI.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a specified location cannot be found.
    /// </summary>
    /// <remarks>Use this exception to indicate that an operation failed because the requested location does
    /// not exist or is unavailable. This exception is typically thrown by methods that require a valid location as
    /// input.</remarks>
    /// <param name="location">The name or identifier of the location that was not found. Cannot be null or empty.</param>
    public class LocationNotFoundException(string location) : Exception($"Location '{location}' not found.")
    {
    }
}
