using JobOffersPortal.Domain.Entities;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Security.Services
{
    public interface IFacebookAuthService
    {
        Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken);
        Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken);
    }
}
