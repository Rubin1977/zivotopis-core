using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZivotopisCore.Models
{
    public class DiagnozaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kód diagnózy je povinný.")]
        [Column(TypeName = "text")]
        public string? Kod { get; set; } // napr. ICD-10

        [Column(TypeName = "text")]
        public required string Nazov { get; set; }

        [Column(TypeName = "text")]
        public string? Popis { get; set; }

        [Column(TypeName = "text")]
        public string? Typ { get; set; } // napr. chronická, akútna

        public bool Aktivna { get; set; } = true;

        public DateTime DátumVytvorenia { get; set; } = DateTime.Now;

        // Navigačná vlastnosť pre Many-to-Many
        public ICollection<PacientModel> Pacienti { get; set; } = new List<PacientModel>();
    }
}
