using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebApp.Services;

namespace JobOffersPortal.UI.Installers
{
    public class HttpClientMvcInstaller : IMvcInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {   
            services.AddHttpClient<IClient, Client>(client => client.BaseAddress = new Uri("https://localhost:5001"));
            services.AddHttpClient<IApiClient, ApiClient>(client => client.BaseAddress = new Uri("https://localhost:5001"));
            services.AddHttpClient<IIdentityClient, IdentityClient>(client => client.BaseAddress = new Uri("https://localhost:5001"));
            services.AddHttpClient<IEmailClient, EmailClient>(client => client.BaseAddress = new Uri("https://localhost:5001"));
            services.AddHttpClient<IAuthClient, AuthClient>(client => client.BaseAddress = new Uri("https://localhost:5001"));
        }
    }
}
