namespace CampusCuisine.Errors
{
    public class BadDataException(string message) : ServiceException(message)
    {

    }
}