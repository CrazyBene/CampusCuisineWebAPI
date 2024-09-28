namespace CampusCuisine.Middlewares
{
    public class LoggingMiddleware
    {

        private readonly RequestDelegate next;
        private readonly ILogger logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            logger.LogInformation($"Request: {context.Request.Path} started.", DateTime.UtcNow.ToLongTimeString());

            await next(context);

            logger.LogInformation($"Request: {context.Request.Path} finished.", DateTime.UtcNow.ToLongTimeString());
        }

    }
}