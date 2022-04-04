using JobOffersPortal.UI.Interfaces;

namespace JobOffersPortal.UI.User
{
    public class DomainUser : IMvcDomainUser
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }
    }
}
