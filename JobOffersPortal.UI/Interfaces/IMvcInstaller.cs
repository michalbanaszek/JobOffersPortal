using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace JobOffersPortal.UI.Interfaces
{
    public interface IMvcInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
