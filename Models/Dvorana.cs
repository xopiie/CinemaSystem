using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uzunova_Nadica_1002387434_DSR_2021.Models
{
    public class Dvorana
    {
        public int Id { get; set; }
        
        public string Naziv { get; set; }

        public int Stevilo_sedezev { get; set; }

        public bool ThreeD { get; set; }

        public string Pot_slike { get; set; }

        public string Opis { get; set; }

    }
}
