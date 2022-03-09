using Hanssens.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApp.ClientServices.Responses;
using WebApp.Interfaces;
using WebApp.Services;

namespace WebApp.ClientServices.Security
{
    public class IdentityMvcService : IIdentityMvcService
    {
        private readonly IIdentityClient _client;
        private readonly LocalStorage _localStorage;

        public IdentityMvcService(IIdentityClient client, LocalStorage localStorage)
        {
            _client = client;
            _localStorage = localStorage;
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

                    _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.Token);

                    return new ResponseFromApi<string>() { Success = true, Data = authResponse.Token };
                }

                return new ResponseFromApi<string>() { Success = false };
            }
            catch (ApiException ex)
            {
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

                    _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.Token);

                    return new ResponseFromApi<string>() { Success = true, Data = authResponse.Token };
                }

                return new ResponseFromApi<string>() { Success = false };
            }
            catch (ApiException ex)
            {
                return new ResponseFromApi<string>() { Success = false, Errors = new[] { ex.Message } };
            }
        }
    }
}
