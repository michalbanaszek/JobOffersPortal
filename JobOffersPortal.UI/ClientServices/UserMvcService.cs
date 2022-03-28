using JobOffersPortal.UI.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebApp.ClientServices.Responses;
using WebApp.Interfaces;

namespace JobOffersPortal.UI.ClientServices
{
    public class UserMvcService : IUserMvcService
    {
        private readonly IApiClient _client;
        private readonly IAddBearerTokenMvcService _addBearerTokenService;
        private readonly ILogger<UserMvcService> _logger;

        public UserMvcService(IApiClient client, IAddBearerTokenMvcService addBearerTokenService, ILogger<UserMvcService> logger)
        {
            _client = client;
            _addBearerTokenService = addBearerTokenService;
            _logger = logger;
        }

        public async Task<ResponseFromApi<UserDetailsViewModel>> GetUserEmailAsync(string userId)
        {
            _addBearerTokenService.AddBearerToken(_client);

            try
            {
                var response = await _client.UserGetAsync(userId);

                if (response == null)
                {
                    return new ResponseFromApi<UserDetailsViewModel>() { Success = false, Errors = new string[] { "UserId is not exists." } };
                }

                return new ResponseFromApi<UserDetailsViewModel> { Success = true, Data = response };
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);

                return new ResponseFromApi<UserDetailsViewModel>() { Success = false, Errors = new string[] { ex.Message } };
            }
        }
    }
}
