using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Threading.Tasks;

namespace JobOffersPortal.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
                var host = CreateHostBuilder(args).Build();

                await host.RunAsync();
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
                .UseSerilog((context, configuration) =>
                {
                    configuration.Enrich.FromLogContext()
                                 .Enrich.WithMachineName()
                                 .WriteTo.Console()
                                 .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticSearchOptions:Uri"]))
                                 {
                                     IndexFormat = $"{context.Configuration["ApplicationName"]}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                                     AutoRegisterTemplate = true,
                                     NumberOfShards = 2,
                                     NumberOfReplicas = 1
                                 })
                                 .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                                 .ReadFrom.Configuration(context.Configuration);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}