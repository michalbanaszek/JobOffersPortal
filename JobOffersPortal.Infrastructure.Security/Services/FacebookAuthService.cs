using JobOffersPortal.Application.Common.Externals.Contracts.Facebook;
using JobOffersPortal.Application.Security.Services;
using JobOffersPortal.Infrastructure.Security.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace JobOffersPortal.Infrastructure.Security.Services
{
    public class FacebookAuthService : IFacebookAuthService
    {
        private const string TokenValidationUrl = "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}";
        private const string UserInfoUrl = "https://graph.facebook.com/me?fields=first_name,last_name,picture,email&access_token={0}";
        private readonly FacebookAuthOptions _facebookAuthOptions;
        private readonly IHttpClientFactory _httpClientFactory;

        public FacebookAuthService(FacebookAuthOptions facebookAuthOptions, IHttpClientFactory httpClientFactory)
        {
            _facebookAuthOptions = facebookAuthOptions;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseFromFacebookApi<FacebookUserInfoResult>> GetUserInfoAsync(string accessToken)
        {
            try
            {
                var formattedUrl = string.Format(UserInfoUrl, accessToken);

                var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);

                result.EnsureSuccessStatusCode();

                var responseAsString = await result.Content.ReadAsStringAsync();

                var response = JsonConvert.DeserializeObject<FacebookUserInfoResult>(responseAsString);

                return new ResponseFromFacebookApi<FacebookUserInfoResult>()
                {
                    Success = true,
                    Data = response
                };
            }
            catch (HttpRequestException exception)
            {
                return new ResponseFromFacebookApi<FacebookUserInfoResult>()
                {
                    Success = false,
                    Errors = new string[] { exception.Message }
                };
            }
        }

        public async Task<ResponseFromFacebookApi<FacebookTokenValidationResult>> ValidateAccessTokenAsync(string accessToken)
        {
            try
            {
                var formattedUrl = string.Format(TokenValidationUrl, accessToken, _facebookAuthOptions.AppId, _facebookAuthOptions.AppSecret);

                var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);

                result.EnsureSuccessStatusCode();

                var responseAsString = await result.Content.ReadAsStringAsync();

                var response = JsonConvert.DeserializeObject<FacebookTokenValidationResult>(responseAsString);

                return new ResponseFromFacebookApi<FacebookTokenValidationResult>()
                {
                    Success = true,
                    Data = response
                };
            }
            catch (HttpRequestException exception)
            {
                return new ResponseFromFacebookApi<FacebookTokenValidationResult>()
                {
                    Success = false,
                    Errors = new string[] { exception.Message }
                };
            }
        }
    }
}
