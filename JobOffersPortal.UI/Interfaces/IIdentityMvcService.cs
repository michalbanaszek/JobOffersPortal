using JobOffersPortal.UI.ClientServices.Responses;
using System.Threading.Tasks;

namespace JobOffersPortal.UI.Interfaces
{
    public interface IIdentityMvcService
    {
        public Task<ResponseFromApi<string>> LoginAsync(string email, string password);
        public Task<ResponseFromApi<string>> RegisterAsync(string email, string password);
        public Task<ResponseFromApi<string>> LoginLdapAsync(string email, string password);
    }
}
