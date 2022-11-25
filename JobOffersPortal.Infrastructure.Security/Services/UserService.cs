using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Domain.Models;
using JobOffersPortal.Infrastructure.Security.User;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersPortal.Infrastructure.Security.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserResult> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            return new UserResult()
            {
                Id = user.Id,
                Email = user.Email
            };
        }

        public async Task<UserResult> GetUserEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return null;
            }

            return new UserResult()
            {
                Id = user.Id,
                Email = email
            };
        }

        public async Task<bool> IsUserEmailAlreadyExistAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user == null ? false : true;
        }

        public async Task<string> CreateUserAsync(string userName, string password)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return null;
            }

            return user.Id;
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return false;
            }

            return true;
        }
    }
}
