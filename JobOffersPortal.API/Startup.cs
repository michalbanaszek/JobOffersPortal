using JobOffersPortal.API.Extensions;
using JobOffersPortal.API.Middleware;
using JobOffersPortal.Application;
using JobOffersPortal.Infrastructure.Security.InfrastructureSecurityInstallation;
using JobOffersPortal.Persistance.EF.InfrastructureInstallation;
using JobOffersPortal.Persistance.EF.Options;
using JobOffersPortal.Persistance.EF.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JobOffersPortal.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services.AddInfrastructureSecurity(Configuration);
            services.InstallServicesInAssembly(Configuration);                     
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContextSeed seeder)
        {
            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            if (env.IsDevelopment())
            {
                seeder.Seed().Wait();

                app.UseDeveloperExceptionPage();                      
                app.UseSwagger(option => { option.RouteTemplate = swaggerOptions.JsonRoute; });
                app.UseSwaggerUI(option => option.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description));
            }          

            app.UseMiddleware<CustomExceptionHandlerMiddleware>();

            app.UseCustomHealthChecks();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseCors("Open");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
