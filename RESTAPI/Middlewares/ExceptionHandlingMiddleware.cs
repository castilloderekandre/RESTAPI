using RESTAPI.Exceptions;
using System.Net;

namespace RESTAPI.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                await HandleExceptionsAsync(httpContext, exception);
            }
        }

        private async Task HandleExceptionsAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An unhandled exception occurred while processing {Method} {Path}.", context.Request.Method, context.Request.Path);

            var (statusCode, message) = exception switch
            {
                LocationNotFoundException ex =>
                    (HttpStatusCode.NotFound, ex.Message),

                WeatherUnavailableException ex =>
                    (HttpStatusCode.ServiceUnavailable, ex.Message),

                ExternalAPIException ex =>
                    (HttpStatusCode.BadGateway, ex.Message),

                _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                error = message,
                status = (int)statusCode
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
