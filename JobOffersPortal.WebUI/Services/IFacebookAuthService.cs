using JobOffersPortal.WebUI.External.Contracts;
using System.Threading.Tasks;

namespace JobOffersPortal.WebUI.Services
{
    public interface IFacebookAuthService
    {
        Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken);

        Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken);
    }
}
