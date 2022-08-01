using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Uzunova_Nadica_1002387434_DSR_2021.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the Uporabnik class
    public class Uporabnik : IdentityUser
    {
        [PersonalData]
        public string Ime { get; set; }

        [PersonalData]
        public string Priimek { get; set; }

        [PersonalData]
        [DataType(DataType.Date)]
        public DateTime DatumRojstva { get; set; }

        [PersonalData]
        public string Naslov { get; set; }

        [PersonalData]
        public string Posta { get; set; }

        [PersonalData]
        [DataType(DataType.PostalCode)]
        public int PostnaStevilka { get; set; }

        [PersonalData]
        public string Drzava { get; set; }


        public static IEnumerable<SelectListItem> GetItems()
        {
            yield return new SelectListItem { Text = "Maribor", Value = "Maribor" };
            yield return new SelectListItem { Text = "Ljubljana", Value = "Ljubljana" };
            yield return new SelectListItem { Text = "Ptuj", Value = "Ptuj" };
            yield return new SelectListItem { Text = "Celje", Value = "Celje" };
            yield return new SelectListItem { Text = "Bled", Value = "Bled" };

        }
    }
}
