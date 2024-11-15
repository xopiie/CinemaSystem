using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Uzunova_Nadica_1002387434_DSR_2021.Areas.Identity.Data;

namespace Uzunova_Nadica_1002387434_DSR_2021.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<Uporabnik> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<Uporabnik> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        /// <summary>
        /// Handles the GET request for the current context.
        /// </summary>
        /// <remarks>
        /// This method is typically used in web applications to respond to HTTP GET requests. 
        /// It can be overridden to include logic that should be executed when a GET request is made, 
        /// such as retrieving data from a database or preparing the model for the view. 
        /// Currently, this implementation does not perform any actions and serves as a placeholder 
        /// for future development. It is essential for maintaining the structure of the application 
        /// and ensuring that the appropriate methods are available for handling web requests.
        /// </remarks>
        public void OnGet()
        {
        }

        /// <summary>
        /// Handles the POST request for logging out a user.
        /// </summary>
        /// <param name="returnUrl">The URL to redirect to after logging out. If null, the user will be redirected to the current page.</param>
        /// <returns>An <see cref="IActionResult"/> that represents the result of the logout operation, which can be a local redirect or a redirect to the current page.</returns>
        /// <remarks>
        /// This method asynchronously signs out the user using the sign-in manager and logs the logout action.
        /// If a return URL is provided, the user is redirected to that URL after logging out.
        /// If no return URL is specified, the user is redirected back to the current page.
        /// This method is typically called when a user initiates a logout action from the application.
        /// </remarks>
        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
