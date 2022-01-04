

using JobOffersPortal.Application.Common.Interfaces;

namespace JobOffersPortal.Infrastructure.Security.Models
{
    public class DomainUser : IDomainUser
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }
    }
}
