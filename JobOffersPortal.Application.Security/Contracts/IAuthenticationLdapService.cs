using JobOffersPortal.Application.Security.Models.LDAP;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Common.Interfaces
{
    public interface IAuthenticationLdapService
    {
        Task<AuthenticationLdapResult> LoginAsync(string username, string password);
    }
}
