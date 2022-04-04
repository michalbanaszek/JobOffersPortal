using Hanssens.Net;
using JobOffersPortal.UI.ClientServices.Responses;
using JobOffersPortal.UI.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace JobOffersPortal.UI.ClientServices.Security
{
    public class IdentityMvcService : IIdentityMvcService
    {
        private readonly IIdentityClient _identityClient;
        private readonly IAuthClient _authClient;
        private readonly LocalStorage _localStorage;
        private readonly ILogger<IdentityMvcService> _logger;

        public IdentityMvcService(IIdentityClient client, LocalStorage localStorage, ILogger<IdentityMvcService> logger, IAuthClient authClient)
        {
            _identityClient = client;
            _localStorage = localStorage;
            _logger = logger;
            _authClient = authClient;
        }

        public async Task<ResponseFromApi<string>> LoginAsync(string email, string password)
        {
            LoginRequest loginRequest = new LoginRequest() { Email = email, Password = password };

            var authResponse = await _identityClient.LoginAsync(loginRequest);

            if (authResponse.Token != string.Empty)
            {
                _localStorage.Store("token", authResponse.Token);

                _logger.LogInformation("Token is generated.");

                return new ResponseFromApi<string>() { Success = true, Data = authResponse.Token };
            }

            _logger.LogError("Token is not generated.");

            return new ResponseFromApi<string>() { Success = false };
        }

        public async Task<ResponseFromApi<string>> LoginLdapAsync(string email, string password)
        {
            LoginLdapRequest loginLdapRequest = new LoginLdapRequest() { Email = email, Password = password };

            var authResponse = await _authClient.LdapAsync(loginLdapRequest);

            if (authResponse.Token != string.Empty)
            {
                _localStorage.Store("token", authResponse.Token);

                _logger.LogInformation("Token is generated.");

                return new ResponseFromApi<string>() { Success = true, Data = authResponse.Token };
            }

            _logger.LogError("Token is not generated.");

            return new ResponseFromApi<string>() { Success = false };
        }

        public async Task<ResponseFromApi<string>> RegisterAsync(string email, string password)
        {

            RegisterRequest registerRequest = new RegisterRequest() { Email = email, Password = password };

            var authResponse = await _identityClient.RegisterAsync(registerRequest);

            if (authResponse.Token != string.Empty)
            {
                _localStorage.Store("token", authResponse.Token);

                _logger.LogInformation("Token is generated.");

                return new ResponseFromApi<string>() { Success = true, Data = authResponse.Token };
            }

            _logger.LogError("Token is not generated.");

            return new ResponseFromApi<string>() { Success = false };
        }
    }
}
