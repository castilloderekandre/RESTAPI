namespace RESTAPI.Exceptions
{
    public class LocationNotFoundException(string location) : Exception($"Location '{location}' not found.")
    {
    }
}
