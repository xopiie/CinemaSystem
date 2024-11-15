using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Uzunova_Nadica_1002387434_DSR_2021.Areas.Identity.Data;

namespace Uzunova_Nadica_1002387434_DSR_2021.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInManager<Uporabnik> _signInManager;
        private readonly UserManager<Uporabnik> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<ExternalLoginModel> _logger;

        public ExternalLoginModel(
            SignInManager<Uporabnik> signInManager,
            UserManager<Uporabnik> userManager,
            ILogger<ExternalLoginModel> logger,
            IEmailSender emailSender)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ProviderDisplayName { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Ime")]
            public string Ime { get; set; }

            [Required]
            [Display(Name = "Priimek")]
            public string Priimek { get; set; }

            [Required]
            [Display(Name = "Naslov")]
            public string Naslov { get; set; }

            [Required]
            [Display(Name = "Pošta")]
            public string Posta { get; set; }

            [Required]
            [DataType(DataType.PostalCode)]
            [Display(Name = "Poštna številka")]
            public int PostnaStevilka { get; set; }

            [Required]
            [Display(Name = "Drzava")]
            public string Drzava { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Datum rojstva")]
            public DateTime DatumRojstva { get; set; }
        }

        /// <summary>
        /// Handles the GET request asynchronously and redirects to the Login page.
        /// </summary>
        /// <returns>An IActionResult that represents the result of the asynchronous operation, which in this case is a redirection to the Login page.</returns>
        /// <remarks>
        /// This method is typically used in a Razor Pages application to manage navigation. 
        /// When a GET request is made to the page, this method is invoked, and it performs a redirection 
        /// to the Login page. This is useful for scenarios where access to certain pages 
        /// should be restricted to authenticated users, thereby directing unauthenticated users 
        /// to the login interface.
        /// </remarks>
        public IActionResult OnGetAsync()
        {
            return RedirectToPage("./Login");
        }

        /// <summary>
        /// Initiates the external login process by redirecting the user to the specified authentication provider.
        /// </summary>
        /// <param name="provider">The name of the external authentication provider to use.</param>
        /// <param name="returnUrl">The URL to redirect to after the external login is complete. This parameter is optional.</param>
        /// <returns>An <see cref="IActionResult"/> that represents the result of the login challenge, which will redirect the user to the external provider.</returns>
        /// <remarks>
        /// This method constructs a redirect URL for the external login callback and configures the authentication properties needed for the external login process.
        /// It uses the <paramref name="provider"/> to identify which external service to authenticate against, such as Google, Facebook, etc.
        /// The <paramref name="returnUrl"/> allows the application to return to a specific page after the authentication process is completed.
        /// If no return URL is provided, the default behavior will be to return to a predefined location.
        /// </remarks>
        public IActionResult OnPost(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        /// <summary>
        /// Handles the callback from an external login provider asynchronously.
        /// </summary>
        /// <param name="returnUrl">The URL to redirect to after the login process is complete. Defaults to the root URL if not provided.</param>
        /// <param name="remoteError">An error message from the external provider, if any.</param>
        /// <returns>An <see cref="IActionResult"/> that represents the result of the callback operation.</returns>
        /// <remarks>
        /// This method processes the callback from an external authentication provider. It first checks for any remote errors and redirects to the login page with an error message if one exists.
        /// If there are no errors, it retrieves the external login information and attempts to sign in the user using that information.
        /// If the sign-in is successful, the user is redirected to the specified return URL. If the user is locked out, they are redirected to a lockout page.
        /// If the user does not have an account, they are prompted to create one, and their email address is pre-filled if available in the external login information.
        /// This method is designed to work with ASP.NET Core Identity and external authentication providers.
        /// </remarks>
        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToPage("./Login", new {ReturnUrl = returnUrl });
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor : true);
            if (result.Succeeded)
            {
                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
                return LocalRedirect(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ReturnUrl = returnUrl;
                ProviderDisplayName = info.ProviderDisplayName;
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    Input = new InputModel
                    {
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                    };
                }
                return Page();
            }
        }

        /// <summary>
        /// Handles the confirmation of an external login and creates a new user account.
        /// </summary>
        /// <param name="returnUrl">The URL to redirect to after confirmation. If null, defaults to the home page.</param>
        /// <returns>An <see cref="IActionResult"/> that represents the result of the confirmation process.</returns>
        /// <remarks>
        /// This method retrieves the external login information and checks if it is valid. If valid, it creates a new user account 
        /// with the provided details and assigns the user to a default role. It also generates an email confirmation token and 
        /// sends a confirmation email to the user. If account confirmation is required, it redirects to a confirmation page. 
        /// If there are any errors during the user creation or login process, those errors are added to the model state for display.
        /// The method also handles the case where external login information cannot be loaded, redirecting to the login page in such cases.
        /// </remarks>
        /// <exception cref="InvalidOperationException">Thrown when external login information cannot be loaded.</exception>
        public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            // Get the information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information during confirmation.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            if (ModelState.IsValid)
            {
                var user = new Uporabnik { UserName = Input.Email, Email = Input.Email, Ime = Input.Ime, Priimek = Input.Priimek, DatumRojstva = Input.DatumRojstva, Naslov = Input.Naslov, Posta = Input.Posta, PostnaStevilka = Input.PostnaStevilka, Drzava = Input.Drzava };

                var result = await _userManager.CreateAsync(user);
                await _userManager.AddToRoleAsync(user, "Gledalec");
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);

                        var userId = await _userManager.GetUserIdAsync(user);
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = userId, code = code },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        // If account confirmation is required, we need to show the link if we don't have a real email sender
                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("./RegisterConfirmation", new { Email = Input.Email });
                        }

                        await _signInManager.SignInAsync(user, isPersistent: false, info.LoginProvider);

                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ProviderDisplayName = info.ProviderDisplayName;
            ReturnUrl = returnUrl;
            return Page();
        }
    }
}
