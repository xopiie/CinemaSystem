using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class LoginWithRecoveryCodeModel : PageModel
    {
        private readonly SignInManager<Uporabnik> _signInManager;
        private readonly ILogger<LoginWithRecoveryCodeModel> _logger;

        public LoginWithRecoveryCodeModel(SignInManager<Uporabnik> signInManager, ILogger<LoginWithRecoveryCodeModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [BindProperty]
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Recovery Code")]
            public string RecoveryCode { get; set; }
        }

        /// <summary>
        /// Handles the GET request for the page and ensures that the user has completed the two-factor authentication process.
        /// </summary>
        /// <param name="returnUrl">An optional URL to redirect to after the operation is complete.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the page operation.</returns>
        /// <remarks>
        /// This method retrieves the current user who is undergoing two-factor authentication using the sign-in manager.
        /// If the user cannot be found, an <see cref="InvalidOperationException"/> is thrown, indicating that the two-factor authentication user could not be loaded.
        /// If the user is successfully retrieved, the method sets the return URL and returns the current page.
        /// This is typically used in scenarios where a user must complete additional verification steps before proceeding.
        /// </remarks>
        /// <exception cref="InvalidOperationException">Thrown when unable to load the two-factor authentication user.</exception>
        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            ReturnUrl = returnUrl;

            return Page();
        }

        /// <summary>
        /// Handles the POST request for two-factor authentication using a recovery code.
        /// </summary>
        /// <param name="returnUrl">The URL to redirect to after successful login. If null, defaults to the home page.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        /// <remarks>
        /// This method first checks if the model state is valid. If not, it returns the current page with validation errors.
        /// It then attempts to retrieve the user associated with two-factor authentication. If the user cannot be loaded,
        /// an <see cref="InvalidOperationException"/> is thrown.
        /// The recovery code is sanitized by removing any spaces before being processed.
        /// The method then attempts to sign in the user using the provided recovery code.
        /// If the sign-in is successful, it logs the event and redirects the user to the specified return URL or the home page.
        /// If the account is locked out, it logs a warning and redirects to the lockout page.
        /// If the recovery code is invalid, it logs a warning, adds a model error, and returns the current page for user correction.
        /// </remarks>
        /// <exception cref="InvalidOperationException">Thrown when unable to load the two-factor authentication user.</exception>
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            var recoveryCode = Input.RecoveryCode.Replace(" ", string.Empty);

            var result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID '{UserId}' logged in with a recovery code.", user.Id);
                return LocalRedirect(returnUrl ?? Url.Content("~/"));
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID '{UserId}' account locked out.", user.Id);
                return RedirectToPage("./Lockout");
            }
            else
            {
                _logger.LogWarning("Invalid recovery code entered for user with ID '{UserId}' ", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid recovery code entered.");
                return Page();
            }
        }
    }
}
