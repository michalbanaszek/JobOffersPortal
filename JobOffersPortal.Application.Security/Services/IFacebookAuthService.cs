using JobOffersPortal.Application.Common.Externals.Contracts.Facebook;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Security.Services
{
    public interface IFacebookAuthService
    {
        Task<ResponseFromFacebookApi<FacebookTokenValidationResult>> ValidateAccessTokenAsync(string accessToken);
        Task<ResponseFromFacebookApi<FacebookUserInfoResult>> GetUserInfoAsync(string accessToken);
    }
}
