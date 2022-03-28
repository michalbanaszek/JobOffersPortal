using Hanssens.Net;
using JobOffersPortal.UI.ClientServices;
using System.Net.Http.Headers;
using WebApp.Interfaces;

namespace WebApp.ClientServices.Security
{
    public class AddBearerTokenService : IAddBearerTokenMvcService
    {
       private readonly LocalStorage _localStorage;

        public AddBearerTokenService(LocalStorage storage)
        {
            _localStorage = storage;
        }

        public void AddBearerToken(IClient client)
        {
            if (_localStorage.Exists("token"))
            {
                client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _localStorage.Get("token").ToString());
            }
        }
    }
}
