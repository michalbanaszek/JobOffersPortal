﻿using JobOffersPortal.WebUI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobOffersPortal.WebUI.Cache
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveSeconds;

        public CachedAttribute(int timeToLiveSeconds)
        {
            _timeToLiveSeconds = timeToLiveSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheSettings = context.HttpContext.RequestServices.GetRequiredService<RedisCacheSettings>();

            if (!cacheSettings.Enabled)
            {
                await next();
                return;
            }

            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cachedResponse = await cacheService.GetCachedResponseAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedResponse))
            {
                var contentResult = new ContentResult()
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentResult;
                return;
            }

            var executedContext = await next();

            if (executedContext.Result is OkObjectResult okObjectResult)
            {
                await cacheService.SetCacheResponseAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLiveSeconds));
            }
        }

        private static string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();

            keyBuilder.Append($"{request.Path}");

            return keyBuilder.ToString();
        }
    }
}
