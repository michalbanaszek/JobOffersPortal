using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticationLdapResult Login(string username, string password);
    }
}
