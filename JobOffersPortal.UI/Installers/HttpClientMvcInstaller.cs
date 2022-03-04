using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;
using System.Net.Http;
using WebApp.Services;

namespace JobOffersPortal.UI.Installers
{
    public class HttpClientMvcInstaller : IMvcInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:5001")
            });

            //services.AddHttpClient("JobOfferApiClient", client =>
            //{
            //    client.BaseAddress = new Uri("https://localhost:5001");
            //}).AddTransientHttpErrorPolicy(x =>
            //    x.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(300)));

            services.AddHttpClient<IApiClient, ApiClient>(client => client.BaseAddress = new Uri("https://localhost:5001"));
            services.AddHttpClient<IIdentityClient, IdentityClient>(client => client.BaseAddress = new Uri("https://localhost:5001"));
            services.AddHttpClient<IEmailClient, EmailClient>(client => client.BaseAddress = new Uri("https://localhost:5001"));
            services.AddHttpClient<IAuthClient, AuthClient>(client => client.BaseAddress = new Uri("https://localhost:5001"));

            //services.AddSingleton(new HttpClient()
            //{
            //    BaseAddress = new Uri("http://main-api")
            //});

            //services.AddHttpClient<IApiClient, ApiClient>(client => client.BaseAddress = new Uri("http://main-api"));
            //services.AddHttpClient<IIdentityClient, IdentityClient>(client => client.BaseAddress = new Uri("http://main-api"));
            //services.AddHttpClient<IEmailClient, EmailClient>(client => client.BaseAddress = new Uri("http://main-api"));
            //services.AddHttpClient<IAuthClient, AuthClient>(client => client.BaseAddress = new Uri("http://main-api"));


        }
    }
}
