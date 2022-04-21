using JobOffersPortal.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
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

                context.Response.StatusCode = 400;

                context.Response.ContentType = JsonContentType;

                await context.Response.WriteAsync(exception.Message);
            }
            catch (UnauthorizedAccessException exception)
            {
                _logger.LogError(exception, exception.Message);

                context.Response.StatusCode = 401;

                context.Response.ContentType = JsonContentType;

                await context.Response.WriteAsync(exception.Message);
            }
            catch (ForbiddenAccessException exception)
            {
                _logger.LogError(exception, exception.Message);

                context.Response.StatusCode = 403;

                context.Response.ContentType = JsonContentType;

                await context.Response.WriteAsync(exception.Message);
            }
            catch (NotFoundException exception)
            {
                _logger.LogError(exception, exception.Message);

                context.Response.StatusCode = 404;

                context.Response.ContentType = JsonContentType;

                await context.Response.WriteAsync(exception.Message);
            }     
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);

                context.Response.StatusCode = 500;

                context.Response.ContentType = JsonContentType;

                await context.Response.WriteAsync(exception.Message);
            }
        }
    }
}