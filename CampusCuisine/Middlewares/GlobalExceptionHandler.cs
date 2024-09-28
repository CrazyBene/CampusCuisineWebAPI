using CampusCuisine.Errors;
using Microsoft.AspNetCore.Diagnostics;

namespace CampusCuisine.Middlewares
{
    public class GlobalExceptionHandler() : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is not ServiceException e)
            {
                return false;
            }

            var statusCode = exception switch
            {
                BadDataException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            await Results.Problem(
                statusCode: statusCode,
                detail: e.ErrorMessage,
                extensions: new Dictionary<string, object?>
                {
                    {"traceId",  context.TraceIdentifier}
                }
            ).ExecuteAsync(context);

            return true;
        }

    }
}