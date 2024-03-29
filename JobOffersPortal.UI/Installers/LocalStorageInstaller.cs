﻿using Hanssens.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobOffersPortal.UI.Installers
{
    public class LocalStorageInstaller : IMvcInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {         
            services.AddSingleton(new LocalStorage());
        }
    }
}
