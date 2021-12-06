﻿using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Persistance.EF.Identity;
using JobOffersPortal.Persistance.EF.Repositories;
using JobOffersPortal.Persistance.EF.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobOffersPortal.Persistance.EF.InfrastructureInstallation
{
    public class ServicesIntaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICompanyRepository, CompanyRepository>();

            services.AddScoped<IJobOfferRepository, JobOfferRepository>();

            services.AddScoped<IJobOfferSkillRepository, JobOfferSkillRepository>();

            services.AddScoped<IJobOfferPropositionRepository, JobOfferPropositionRepository>();

            services.AddScoped<IJobOfferRequirementRepository, JobOfferRequirementRepository>();

            services.AddTransient<IDateTime, DateTimeService>();

            services.AddScoped<IIdentityService, IdentityService>();

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