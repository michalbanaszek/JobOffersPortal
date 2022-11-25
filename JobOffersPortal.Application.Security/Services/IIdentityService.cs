using JobOffersPortal.Domain.Models;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Security.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
        Task<AuthenticationResult> LoginAsync(string email, string password);
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
        Task<AuthenticationResult> LoginFacebookAsync(string accessToken);
        Task<AuthenticationResult> LoginLdap(string email, string password);
    }
}
