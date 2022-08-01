using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Uzunova_Nadica_1002387434_DSR_2021.Models
{
    public class Film
    {
        public int Id { get; set; } //primary key

        [StringLength(60, MinimumLength = 3)]
        [DataType(DataType.Text)]
        [RegularExpression("^[a-zA-Z*]+$", ErrorMessage = "Vnesite samo črke!")]
        [Required(ErrorMessage = "Polje je obvezno!")]
        public string Naslov { get; set; }

        [StringLength(25, MinimumLength = 3)]
        [DataType(DataType.Text)]
        [RegularExpression("^[a-zA-Z*]+$", ErrorMessage = "Vnesite samo črke!")]
        [Required(ErrorMessage = "Polje je obvezno!")]
        public string Jezik { get; set; }

        [Required(ErrorMessage = "Polje je obvezno!")]
        //[CustomValidation(typeof(PodatkiZaRegistracija), "ValidcijaDatuma")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatumIzida { get; set; }

        public static ValidationResult ValidcijaDatuma(object datum)
        {

            if (datum == null)
                return new ValidationResult("*");

            DateTime date;
            DateTime min = new DateTime(1900, 1, 1);
            DateTime max = DateTime.Now;
            bool datumJePravilen = DateTime.TryParse(datum.ToString(), out date) ? true : false;
            if (datumJePravilen)
            {
                if ((date > min) && (date < max))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Vnesite datum pravilno!");
                }
            }
            else
            {
                return new ValidationResult("Vnesite datum pravilno!");
            }
        }

        [Required(ErrorMessage = "Polje je obvezno!")]
        // [DataType(DataType.Text)]
        [RegularExpression("^[0-9]{1,3}$", ErrorMessage = "Vnesite samo števila!")]
        public Int32 Trajanje { get; set; }


        [Required(ErrorMessage = "Polje je obvezno!")]
        //[DataType(DataType.Text)]
        public Double Ocena { get; set; }

        public string Opis { get; set; }

        public string Pot_slike { get; set; }

    }
}
