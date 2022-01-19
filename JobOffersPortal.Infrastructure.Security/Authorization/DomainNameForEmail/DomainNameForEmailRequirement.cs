using Microsoft.AspNetCore.Authorization;

namespace JobOffersPortal.Infrastructure.Security.Authorization.DomainNameForEmail
{
    public class DomainNameForEmailRequirement : IAuthorizationRequirement
    {
        public string DomainName { get; }

        public DomainNameForEmailRequirement(string domainName)
        {
            DomainName = domainName;
        }
    }
}