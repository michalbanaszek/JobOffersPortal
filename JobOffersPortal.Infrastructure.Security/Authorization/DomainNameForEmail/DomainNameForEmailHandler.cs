using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JobOffersPortal.Infrastructure.Security.Authorization.DomainNameForEmail
{
    public class DomainNameForEmailHandler : AuthorizationHandler<DomainNameForEmailRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DomainNameForEmailRequirement requirement)
        {
            var userEmailAddress = context.User?.FindFirstValue(ClaimTypes.Email) ?? string.Empty;

            if (userEmailAddress.EndsWith(requirement.DomainName))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}