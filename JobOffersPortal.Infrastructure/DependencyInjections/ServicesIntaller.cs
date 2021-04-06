using Application.Common.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.DependencyInjections
{
    public class ServicesIntaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICompanyService, CompanyService>();

            services.AddScoped<IJobOfferService, JobOfferService>();

            services.AddSingleton<ICacheService, RedisCacheService>();

            services.AddTransient<IDateTime, DateTimeService>();

            services.AddScoped<IIdentityService, IdentityService>();

            services.AddSingleton<IFacebookAuthService, FacebookAuthService>();

            services.AddSingleton<IUriService>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://" + request.Host.ToUriComponent(), "/");
                return new UriService(absoluteUri);
            });

            services.AddSingleton<IUriCompanyService>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://" + request.Host.ToUriComponent(), "/");
                return new UriCompanyService(absoluteUri);
            });

            services.AddSingleton<IUriJobOfferService>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://" + request.Host.ToUriComponent(), "/");
                return new UriJobOfferService(absoluteUri);
            });
        }
    }
}
