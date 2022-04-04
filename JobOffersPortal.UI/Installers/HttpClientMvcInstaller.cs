using JobOffersPortal.UI.ClientServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;

namespace JobOffersPortal.UI.Installers
{
    public class HttpClientMvcInstaller : IMvcInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSingleton(new HttpClient()
            //{
            //    BaseAddress = new Uri("https://localhost:5001")
            //});

            //services.AddHttpClient<IApiClient, ApiClient>(client => client.BaseAddress = new Uri("https://localhost:5001")).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            //{
            //    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            //});

            //services.AddHttpClient<IIdentityClient, IdentityClient>(client => client.BaseAddress = new Uri("https://localhost:5001")).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            //{
            //    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            //});

            //services.AddHttpClient<IEmailClient, EmailClient>(client => client.BaseAddress = new Uri("https://localhost:5001")).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            //{
            //    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            //});

            //services.AddHttpClient<IAuthClient, AuthClient>(client => client.BaseAddress = new Uri("https://localhost:5001")).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            //{
            //    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            //});

            services.AddSingleton(new HttpClient()
            {
                BaseAddress = new Uri("https://main-api:443")
            });
            services.AddHttpClient<IApiClient, ApiClient>(client => client.BaseAddress = new Uri("https://main-api:443")).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }

            });
            services.AddHttpClient<IIdentityClient, IdentityClient>(client => client.BaseAddress = new Uri("https://main-api:443")).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });
            services.AddHttpClient<IEmailClient, EmailClient>(client => client.BaseAddress = new Uri("https://main-api:443")).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });
            services.AddHttpClient<IAuthClient, AuthClient>(client => client.BaseAddress = new Uri("https://main-api:443")).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });
        }
    }
}