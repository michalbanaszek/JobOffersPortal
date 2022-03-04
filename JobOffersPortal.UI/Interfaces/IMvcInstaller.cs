using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace WebApp.Interfaces
{
    public interface IMvcInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
