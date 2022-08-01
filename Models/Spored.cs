using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Uzunova_Nadica_1002387434_DSR_2021.Models
{
    public class Spored
    {
        public int Id { get; set; }

       // public int Id_Film_tk { get; set; }

        public string NaslovFilma { get; set; }

        //public int Id_Dvorana_tk { get; set; }

        public string NazivDvorane { get; set; }

        public DateTime DatumCas { get; set; }

        [NotMapped]
        public int ProsteSedeze { get; set; }        

    }

    [NotMapped]
    public class SporedDodaj : Spored
    {
        public List<string> Filmi { get; set; } = new List<string>();

        public List<string> Dvorane { get; set; } = new List<string>();
    }
}
