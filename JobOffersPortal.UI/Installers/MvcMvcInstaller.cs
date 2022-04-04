using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobOffersPortal.UI.Installers
{
    public class MvcMvcInstaller : IMvcInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc()
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
        }
    }
}
