namespace CampusCuisine.Errors
{
    public class NotFoundException(string message) : ServiceException(message)
    {

    }
}