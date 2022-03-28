namespace JobOffersPortal.Infrastructure.Security.Contracts.OAuth.Ldap.Requests
{
    public class LoginLdapRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
