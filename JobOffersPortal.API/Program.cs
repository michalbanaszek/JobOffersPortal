using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using JobOffersPortal.Persistance.EF.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using NLog.Web;
using System.Threading.Tasks;
using NLog;

namespace JobOffersPortal.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

            try
            {
                logger.Debug("init main function");

                var host = CreateHostBuilder(args).Build();

                using (var serviceScope = host.Services.CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    if (context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                    {
                        await context.Database.MigrateAsync();
                    }

                    await ApplicationDbContextSeed.Initialize(serviceScope);
                }

                await host.RunAsync();
            }
            catch (Exception exception)
            {              
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {              
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseMetricsWebTracking()
                .UseMetrics(options =>
                {
                    options.EndpointOptions = endpointsOptions =>
                    {
                        endpointsOptions.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
                        endpointsOptions.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
                        endpointsOptions.EnvironmentInfoEndpointEnabled = false;
                    };
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
               .ConfigureLogging(logging =>
               {
                   logging.ClearProviders();
                   logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
               })
                .UseNLog();  
    }
}