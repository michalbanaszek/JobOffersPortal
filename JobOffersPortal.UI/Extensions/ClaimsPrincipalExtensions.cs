using System.Linq;
using System.Security.Claims;

namespace JobOffersPortal.UI.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Claim GetClaim(this ClaimsPrincipal user, string claimType)
        {
            return user.Claims
                .SingleOrDefault(c => c.Type == claimType);
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {
            var claim = user.GetClaim(ClaimTypes.Email);

            return claim?.Value;
        }
    }
}
