using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp.Controllers
{
    [Authorize(Policy = "LdapPolicy", AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class WebControllerBase : Controller
    {    
        private IMapper _mapper;
        private ISender _sender;
        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();
        protected ISender Mediator => _sender ??= HttpContext.RequestServices.GetService<ISender>();
    }
}
