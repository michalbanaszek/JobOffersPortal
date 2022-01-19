using System.Threading.Tasks;

namespace JobOffersPortal.Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<string> GetUserNameAsync(string userId);
        Task<bool> IsInRoleAsync(string userId, string role);
        Task<bool> CreateUserAsync(string userName, string password);
        Task<bool> DeleteUserAsync(string userId);
    }
}
