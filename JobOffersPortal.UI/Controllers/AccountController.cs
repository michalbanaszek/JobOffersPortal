using Hanssens.Net;
using JobOffersPortal.UI.Interfaces;
using JobOffersPortal.UI.ViewModels.AccountMvc.LoginAccountMvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JobOffersPortal.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationLdapMvcService _authenticationLdapService;
        private readonly LocalStorage _localStorage;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAuthenticationLdapMvcService authenticationLdapService, IIdentityMvcService identityMvcService, LocalStorage localStorage, ILogger<AccountController> logger)
        {
            _authenticationLdapService = authenticationLdapService;
            _localStorage = localStorage;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginMvcViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _authenticationLdapService.LoginAsync(viewModel.Username, viewModel.Password);

                    // If the user is authenticated, store its claims to cookie
                    if (response.Success)
                    {
                        await HttpContext.SignInAsync(
                          CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(response.ClaimsIdentity),
                            new AuthenticationProperties
                            {
                                IsPersistent = viewModel.RememberMe
                            }
                        );

                        return Redirect(Url.IsLocalUrl(viewModel.ReturnUrl)
                            ? viewModel.ReturnUrl
                            : "/");
                    }

                    ModelState.AddModelError("", @"Your username or password is incorrect. Please try again.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);

                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(new LoginMvcViewModel() { Username = viewModel.Username, Password = viewModel.Password, RememberMe = viewModel.RememberMe, ReturnUrl = viewModel.ReturnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            _localStorage.Remove("token");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}