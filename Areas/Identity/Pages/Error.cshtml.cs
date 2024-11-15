using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Uzunova_Nadica_1002387434_DSR_2021.Areas.Identity.Pages
{
    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        /// <summary>
        /// Handles the GET request for the current context.
        /// </summary>
        /// <remarks>
        /// This method is invoked when a GET request is made to the associated endpoint. 
        /// It retrieves the current activity's identifier, which can be useful for tracing and logging purposes. 
        /// If there is no current activity, it falls back to using the trace identifier from the HTTP context. 
        /// This ensures that every request can be uniquely identified, aiding in debugging and monitoring.
        /// </remarks>
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}
