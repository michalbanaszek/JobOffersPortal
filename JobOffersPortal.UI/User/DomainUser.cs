using WebApp.Interfaces;

namespace WebApp.Models
{
    public class DomainUser : IMvcDomainUser
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }
    }
}
