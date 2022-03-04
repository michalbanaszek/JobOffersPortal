using System.Threading.Tasks;
using WebApp.ClientServices.Responses;

namespace WebApp.Interfaces
{
    public interface IIdentityMvcService
    {
        public Task<ResponseFromApi<string>> LoginAsync(string email, string password);
        public Task<ResponseFromApi<string>> RegisterAsync(string email, string password);
    }
}
