using System.Threading.Tasks;
using WebApp.Domain;

namespace WebApp.Interfaces
{
    public interface IAuthenticationLdapMvcService
    {
        Task<AuthenticationLdapResult> LoginAsync(string username, string password);
    }
}
