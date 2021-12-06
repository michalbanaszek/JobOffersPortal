using JobOffersPortal.Application.Common.Models.Responses;

namespace JobOffersPortal.Application.Common.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticationLdapResult Login(string username, string password);
    }
}
