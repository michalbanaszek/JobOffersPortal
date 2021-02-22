using JobOffersPortal.WebUI.Domain;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace JobOffersPortal.WebUI.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
        Task<AuthenticationResult> LoginAsync(string email, string password);
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
    }
}
