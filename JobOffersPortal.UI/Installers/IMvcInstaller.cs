using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobOffersPortal.UI.Installers
{
    public interface IMvcInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}