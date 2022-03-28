using JobOffersPortal.Domain.Models;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<UserResult> GetUserByIdAsync(string userId);
        Task<UserResult> GetUserEmailAsync(string email);
        Task<bool> IsUserEmailAlreadyExistAsync(string email);
        Task<bool> IsInRoleAsync(string userId, string role);
        Task<string> CreateUserAsync(string userName, string password);
        Task<bool> DeleteUserAsync(string email);
    }
}
