using JobOffersPortal.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace JobOffersPortal.API.Middleware
{
    public class CustomExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
        private const string JsonContentType = "application/json";

        public CustomExceptionHandlerMiddleware(ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (ValidationCustomException exception)
            {
                _logger.LogError(exception, exception.Message);

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = JsonContentType;

                var details = new ValidationProblemDetails(exception.Errors)
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = exception.Message,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                };

                var serializedData = JsonConvert.SerializeObject(details);

                await context.Response.WriteAsync(serializedData);

                context.Response.Headers.Clear();
            }
            catch (UnauthorizedAccessException exception)
            {
                _logger.LogError(exception, exception.Message);

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = JsonContentType;

                var details = new ProblemDetails
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Title = "Unauthorized",
                    Detail= exception.Message,
                    Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
                };

                var serializedData = JsonConvert.SerializeObject(details);

                await context.Response.WriteAsync(serializedData);

                context.Response.Headers.Clear();
            }
            catch (ForbiddenAccessException exception)
            {
                _logger.LogError(exception, exception.Message);

                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = JsonContentType;

                var details = new ProblemDetails
                {
                    Status = StatusCodes.Status403Forbidden,
                    Title = "Forbidden",
                    Detail = exception.Message,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
                };

                var serializedData = JsonConvert.SerializeObject(details);

                await context.Response.WriteAsync(serializedData);

                context.Response.Headers.Clear();
            }
            catch (NotFoundException exception)
            {
                _logger.LogError(exception, exception.Message);

                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.ContentType = JsonContentType;

                var details = new ProblemDetails()
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "The specified resource was not found.",
                    Detail = exception.Message
                };

                var serializedData = JsonConvert.SerializeObject(details);

                await context.Response.WriteAsync(serializedData);

                context.Response.Headers.Clear();
            }     
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = JsonContentType;

                var details = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "An error occurred while processing your request.",
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
                };

                var serializedData = JsonConvert.SerializeObject(details);

                await context.Response.WriteAsync(serializedData);

                context.Response.Headers.Clear();
            }
        }
    }
}