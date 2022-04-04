using JobOffersPortal.UI.ClientServices;
using System.Threading.Tasks;

namespace JobOffersPortal.UI.Interfaces
{
    public interface IUserMvcService
    {
        Task<UserDetailsViewModel> GetUserEmailAsync(string email);      
    }
}
