using JobOffersPortal.UI.Domain;
using System.Threading.Tasks;

namespace JobOffersPortal.UI.Interfaces
{
    public interface IAuthenticationLdapMvcService
    {
        Task<AuthenticationLdapResult> LoginAsync(string username, string password);
    }
}
