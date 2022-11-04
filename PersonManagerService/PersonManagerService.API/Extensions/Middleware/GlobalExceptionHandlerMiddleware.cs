using System.Text.Json;

namespace PersonManagerService.API.Extensions.Middleware
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                var responseError = new CustomResponseErrorDetails(System.Net.HttpStatusCode.InternalServerError, "Server error", e.Message);
                string jsonResponse = JsonSerializer.Serialize(responseError);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
