using JobOffersPortal.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace JobOffersPortal.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue("id");
        public string UserName => _httpContextAccessor.HttpContext?.User?.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).First().Value;
    }
}
