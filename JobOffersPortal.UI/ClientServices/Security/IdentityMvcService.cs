using Hanssens.Net;
using JobOffersPortal.UI.ClientServices;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebApp.ClientServices.Responses;
using WebApp.Interfaces;

namespace WebApp.ClientServices.Security
{
    public class IdentityMvcService : IIdentityMvcService
    {
        private readonly IIdentityClient _client;
        private readonly LocalStorage _localStorage;
        private readonly ILogger<IdentityMvcService> _logger;

        public IdentityMvcService(IIdentityClient client, LocalStorage localStorage, ILogger<IdentityMvcService> logger)
        {
            _client = client;
            _localStorage = localStorage;
            _logger = logger;
        }

        public async Task<ResponseFromApi<string>> LoginAsync(string email, string password)
        {
            try
            {
                LoginRequest loginRequest = new LoginRequest() { Email = email, Password = password };

                var authResponse = await _client.LoginAsync(loginRequest);

                if (authResponse.Token != string.Empty)
                {
                    _localStorage.Store("token", authResponse.Token);

                    return new ResponseFromApi<string>() { Success = true, Data = authResponse.Token };
                }

                return new ResponseFromApi<string>() { Success = false };
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);

                return new ResponseFromApi<string>() { Success = false, Errors = new[] { ex.Message } };
            }
        }

        public async Task<ResponseFromApi<string>> LoginLdapAsync(string email, string password)
        {

            try
            {
                LoginLdapRequest loginLdapRequest = new LoginLdapRequest() { Email = email, Password = password };

                var authResponse = await _client.LoginLdapAsync(loginLdapRequest);

                if (authResponse.Token != string.Empty)
                {
                    _localStorage.Store("token", authResponse.Token);

                    return new ResponseFromApi<string>() { Success = true, Data = authResponse.Token };
                }

                return new ResponseFromApi<string>() { Success = false };
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);

                return new ResponseFromApi<string>() { Success = false, Errors = new[] { ex.Message } };
            }
        }

        public async Task<ResponseFromApi<string>> RegisterAsync(string email, string password)
        {
            try
            {
                RegisterRequest registerRequest = new RegisterRequest() { Email = email, Password = password };

                var authResponse = await _client.RegisterAsync(registerRequest);

                if (authResponse.Token != string.Empty)
                {
                    _localStorage.Store("token", authResponse.Token);

                    return new ResponseFromApi<string>() { Success = true, Data = authResponse.Token };
                }

                return new ResponseFromApi<string>() { Success = false };
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);

                return new ResponseFromApi<string>() { Success = false, Errors = new[] { ex.Message } };
            }
        }
    }
}
