using Microsoft.AspNetCore.Http;
using System.Linq;

namespace JobOffersPortal.WebUI.Extensions
{
    public static class GeneralExtensions
    {
        public static string GetUserId(this HttpContext context)
        {
            if (context.User == null)
            {
                return string.Empty;
            }

            return context.User.Claims.Single(x => x.Type == "id").Value;
                
        }
    }
}
