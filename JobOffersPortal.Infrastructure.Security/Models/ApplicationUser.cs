using JobOffersPortal.Application.Security.Contracts;
using Microsoft.AspNetCore.Identity;

namespace JobOffersPortal.Infrastructure.Security.Models
{
    public class ApplicationUser : IdentityUser, IApplicationUser
    {
    }
}
