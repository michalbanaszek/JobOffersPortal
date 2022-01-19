using JobOffersPortal.Application.Security.Interfaces.User;
using Microsoft.AspNetCore.Identity;

namespace JobOffersPortal.Infrastructure.Security.User
{
    public class ApplicationUser : IdentityUser, IApplicationUser
    {
    }
}
