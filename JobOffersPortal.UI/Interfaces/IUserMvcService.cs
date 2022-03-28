using JobOffersPortal.UI.ClientServices;
using System.Threading.Tasks;
using WebApp.ClientServices.Responses;

namespace JobOffersPortal.UI.Interfaces
{
    public interface IUserMvcService
    {
        Task<ResponseFromApi<UserDetailsViewModel>> GetUserEmailAsync(string email);      
    }
}
