using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Uzunova_Nadica_1002387434_DSR_2021.Models
{
    public class Rezervacija
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("idSpored")]
        public int IdSpored { get; set; }

        [JsonPropertyName("steviloSedezev")]
        public int SteviloSedezev { get; set; }

        [NotMapped]
        [JsonPropertyName("film")]
        public string Film { get; set; }

        [NotMapped]
        [JsonPropertyName("dvorana")]
        public string Dvorana { get; set; }

        [NotMapped]
        [JsonPropertyName("datumCas")]
        public DateTime DatumCas { get; set; }

    }
}
