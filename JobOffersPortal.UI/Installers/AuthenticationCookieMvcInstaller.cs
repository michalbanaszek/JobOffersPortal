﻿using JobOffersPortal.UI.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobOffersPortal.UI.Installers
{
    public class AuthenticationCookieMvcInstaller : IMvcInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var cookiesConfig = configuration.GetSection("cookies")
                                             .Get<CookiesOptions>();

            services.AddAuthentication(
                  CookieAuthenticationDefaults.AuthenticationScheme)
                  .AddCookie(options =>
                  {
                      options.Cookie.Name = cookiesConfig.CookieName;
                      options.LoginPath = cookiesConfig.LoginPath;
                      options.LogoutPath = cookiesConfig.LogoutPath;
                      options.AccessDeniedPath = cookiesConfig.AccessDeniedPath;
                      options.ReturnUrlParameter = cookiesConfig.ReturnUrlParameter;
                  });
        }
    }
}