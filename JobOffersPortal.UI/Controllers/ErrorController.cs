using JobOffersPortal.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JobOffersPortal.UI.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this._logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult StatusCodeHandler(int statusCode)
        {
            var statusCodeResult =
              HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found";

                    this._logger.LogWarning($"404 error occured. Path = {statusCodeResult.OriginalPath}" + $" and QueryString = {statusCodeResult.OriginalQueryString}");
                    break;
            }
            return View("NotFound");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature =
                    HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            this._logger.LogError($"The path {exceptionHandlerPathFeature.Path} threw an exception {exceptionHandlerPathFeature.Error.Message}");

            return View("Error", new ErrorViewModel());
        }

    }
}
