namespace CampusCuisine.Errors
{
    public class ForbiddenException(string message) : ServiceException(message)
    {

    }
}
