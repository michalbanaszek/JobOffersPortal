using JobOffersPortal.UI.Interfaces;
using System.Threading.Tasks;

namespace JobOffersPortal.UI.ClientServices
{
    public class UserMvcService : IUserMvcService
    {
        private readonly IApiClient _client;
        private readonly IAddBearerTokenMvcService _addBearerTokenService;     

        public UserMvcService(IApiClient client, IAddBearerTokenMvcService addBearerTokenService)
        {
            _client = client;
            _addBearerTokenService = addBearerTokenService;     
        }

        public async Task<UserDetailsViewModel> GetUserEmailAsync(string userId)
        {
            _addBearerTokenService.AddBearerToken(_client);

            var response = await _client.UserGetAsync(userId);

            return response;
        }
    }
}
