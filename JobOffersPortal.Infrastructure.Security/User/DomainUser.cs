using JobOffersPortal.Application.Security.Interfaces.User;

namespace JobOffersPortal.Infrastructure.Security.User
{
    public class DomainUser : IDomainUser
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }
    }
}
