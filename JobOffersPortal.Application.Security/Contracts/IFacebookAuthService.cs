using JobOffersPortal.Application.Security.Models.External;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Common.Interfaces
{
    public interface IFacebookAuthService
    {
        Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken);
        Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken);
    }
}
