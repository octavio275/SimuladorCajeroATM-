using EjercicioPOO.Application.Dto;
using EjercicioPOO.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EjercicioPOO.Application.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (InternalErrorException internalError)
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                _logger.LogError($"Something went wrong: {internalError}");
                await HandleExceptionAsync(httpContext, internalError);
            }
            catch (NotFoundException notFound)
            {
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                _logger.LogError($"The entry cannot be found: {notFound}");
                await HandleExceptionAsync(httpContext, notFound);
            }
            catch (BadRequestException badRequest)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                _logger.LogError($"Bad request: {badRequest}");
                await HandleExceptionAsync(httpContext, badRequest);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }
    }
}
