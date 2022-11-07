using FluentValidation;
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
            catch (ValidationException validationException)
            {
                _logger.LogError(validationException, validationException.Message);

                var responseError = new CustomResponseErrorDetails(System.Net.HttpStatusCode.BadRequest, "Bad request error", validationException.Message, validationException.Errors);
                await WriteResponseAsync(context, responseError, System.Net.HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                var responseError = new CustomResponseErrorDetails(System.Net.HttpStatusCode.InternalServerError, "Server error", e.Message);
                await WriteResponseAsync(context, responseError, System.Net.HttpStatusCode.InternalServerError);
            }
        }

        private async Task WriteResponseAsync(HttpContext context, CustomResponseErrorDetails customResponseErrorDetails, System.Net.HttpStatusCode statusCode)
        {
            string jsonResponse = JsonSerializer.Serialize(customResponseErrorDetails);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
