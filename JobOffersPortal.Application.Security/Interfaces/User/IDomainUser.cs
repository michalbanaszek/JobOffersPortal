namespace JobOffersPortal.Application.Security.Interfaces.User
{
    public interface IDomainUser
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }
    }
}
