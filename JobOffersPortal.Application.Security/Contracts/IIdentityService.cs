using JobOffersPortal.Application.Common.Models;
using JobOffersPortal.Application.Security.Models.AuthResult;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Security.Contracts
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);
        Task<bool> IsInRoleAsync(string userId, string role);
        Task<bool> AuthorizeAsync(string userId, string policyName);
        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);
        Task<Result> DeleteUserAsync(string userId);
        Task<AuthenticationResult> RegisterAsync(string email, string password);
        Task<AuthenticationResult> LoginAsync(string email, string password);
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
        Task<AuthenticationResult> LoginWithFacebookAsync(string accessToken);
    }
}
