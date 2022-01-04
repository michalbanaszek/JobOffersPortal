namespace JobOffersPortal.Application.Security.Models.LDAP
{
    public class LoginLdapRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}