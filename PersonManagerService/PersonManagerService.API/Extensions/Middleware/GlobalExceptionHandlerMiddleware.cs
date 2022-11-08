using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using ValidationException = FluentValidation.ValidationException;

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
                await WriteErrorResponseAsync(e, context);
            }
        }

        private async Task WriteErrorResponseAsync(Exception exception, HttpContext context)
        {
            _logger.LogError(exception, exception.Message);

            CustomResponseErrorDetails errorDetails = exception switch
            {
                ValidationException validationException => new CustomResponseErrorDetails(HttpStatusCode.BadRequest, "Bad request error", validationException.Message, validationException.Errors),
                _ => new CustomResponseErrorDetails(HttpStatusCode.InternalServerError, "Server error", exception.Message),
            };

            string jsonResponse = JsonSerializer.Serialize(errorDetails);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)errorDetails.StatusCode;
            await context.Response.WriteAsync(jsonResponse);
        }
    }

}
