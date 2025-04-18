using Domain.Exceptions;
using Shared.ErrorModels;

namespace Ecommerce_Store.API.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);

                if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                    await HandlingNotFoundEndPointAsync(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");

                await HandlingErrorAsync(context, ex);
            }
        }

        private static async Task HandlingErrorAsync(HttpContext context, Exception ex)
        {
            //context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new ErrorDetails
            {
                //statusCode = context.Response.StatusCode,
                ErrorMessage = ex.Message
            };

            // Check if the exception is a known type and set the status code accordingly
            response.statusCode = ex switch
            {
                // Add more specific exceptions here if needed
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            await context.Response.WriteAsJsonAsync(response);
        }

        private static async Task HandlingNotFoundEndPointAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails
            {
                statusCode = StatusCodes.Status404NotFound,
                ErrorMessage = $"EndPoint {context.Request.Path} is not found"
            };
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
