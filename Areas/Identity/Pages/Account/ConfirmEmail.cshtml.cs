using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Uzunova_Nadica_1002387434_DSR_2021.Areas.Identity.Data;

namespace Uzunova_Nadica_1002387434_DSR_2021.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<Uporabnik> _userManager;

        public ConfirmEmailModel(UserManager<Uporabnik> userManager)
        {
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        /// Asynchronously handles the GET request for confirming a user's email address.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose email is being confirmed.</param>
        /// <param name="code">The confirmation code that has been sent to the user's email.</param>
        /// <returns>An <see cref="IActionResult"/> that represents the result of the email confirmation process.</returns>
        /// <remarks>
        /// This method first checks if the provided <paramref name="userId"/> or <paramref name="code"/> is null. 
        /// If either is null, it redirects the user to the index page. 
        /// It then attempts to find the user associated with the given <paramref name="userId"/>. 
        /// If the user cannot be found, a 404 Not Found result is returned with an appropriate message. 
        /// If the user is found, the method decodes the confirmation code from Base64 URL format and 
        /// calls the <see cref="_userManager.ConfirmEmailAsync"/> method to confirm the user's email address.
        /// Depending on whether the confirmation was successful, a status message is set to inform the user.
        /// Finally, it returns the current page with the status message displayed.
        /// </remarks>
        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
            return Page();
        }
    }
}
