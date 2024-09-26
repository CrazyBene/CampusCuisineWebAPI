namespace CampusCuisine.Errors
{
    public class ServiceException(string message) : Exception
    {

        public string ErrorMessage { get; } = message;

    }
}