using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Uzunova_Nadica_1002387434_DSR_2021.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordConfirmation : PageModel
    {
        /// <summary>
        /// Handles the GET request for the page.
        /// </summary>
        /// <remarks>
        /// This method is invoked when a GET request is made to the page. 
        /// It is typically used to initialize data or perform actions that need to occur 
        /// when the page is first loaded. Since this method does not take any parameters 
        /// or return any values, it serves as a placeholder for any setup logic that 
        /// may be required before rendering the page.
        /// </remarks>
        public void OnGet()
        {
        }
    }
}
