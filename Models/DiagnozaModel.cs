using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZivotopisCore.Models
{
    public class DiagnozaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kód diagnózy je povinný.")]
        public string? Kod { get; set; } // napr. ICD-10
        public required string Nazov { get; set; }

        public string? Popis { get; set; }
        public string? Typ { get; set; } // napr. chronická, akútna

        public bool Aktivna { get; set; } = true;
        public DateTime DatumVytvorenia { get; set; }

        // Navigačná vlastnosť pre Many-to-Many
        public ICollection<PacientModel> Pacienti { get; set; } = new List<PacientModel>();
    }
}
