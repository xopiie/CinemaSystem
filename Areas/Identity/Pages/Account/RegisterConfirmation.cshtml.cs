using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Uzunova_Nadica_1002387434_DSR_2021.Areas.Identity.Data;

namespace Uzunova_Nadica_1002387434_DSR_2021.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<Uporabnik> _userManager;
        private readonly IEmailSender _sender;

        public RegisterConfirmationModel(UserManager<Uporabnik> userManager, IEmailSender sender)
        {
            _userManager = userManager;
            _sender = sender;
        }

        public string Email { get; set; }

        public bool DisplayConfirmAccountLink { get; set; }

        public string EmailConfirmationUrl { get; set; }

        /// <summary>
        /// Handles the GET request for the page, retrieving user information based on the provided email.
        /// </summary>
        /// <param name="email">The email address of the user to be loaded.</param>
        /// <param name="returnUrl">An optional return URL to redirect to after processing.</param>
        /// <returns>An <see cref="IActionResult"/> that represents the result of the asynchronous operation.</returns>
        /// <remarks>
        /// This method first checks if the provided email is null; if it is, it redirects to the Index page.
        /// It then attempts to find the user associated with the given email using the user manager.
        /// If the user is not found, it returns a NotFound result with an appropriate message.
        /// If the user is found, it sets the Email property and prepares a confirmation link for the account.
        /// The confirmation link is generated using a token that is encoded in a URL-safe format.
        /// Finally, it returns the current page result.
        /// </remarks>
        public async Task<IActionResult> OnGetAsync(string email, string returnUrl = null)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            Email = email;
            // Once you add a real email sender, you should remove this code that lets you confirm the account
            DisplayConfirmAccountLink = true;
            if (DisplayConfirmAccountLink)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                EmailConfirmationUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                    protocol: Request.Scheme);
            }

            return Page();
        }
    }
}
