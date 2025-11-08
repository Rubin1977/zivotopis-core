using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZivotopisCore.Models
{
    public class PacientModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Meno je povinné")]
        public required string Meno { get; set; }

        [Required(ErrorMessage = "Priezvisko je povinné")]
        public required string Priezvisko { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Dátum narodenia je povinný")]
        public DateTime DatumNarodenia { get; set; }

        [Required(ErrorMessage = "Pohlavie je povinné")]
        public required string Pohlavie { get; set; }

        [Display(Name = "Rodné číslo")]
        [Required(ErrorMessage = "Rodné číslo je povinné")]
        public required string RodneCislo { get; set; }

        public bool Archivovany { get; set; } = false;


        // Diagnózy pacienta
        public virtual ICollection<DiagnozaModel> Diagnozy { get; set; }

        // Pripravené na budúce rozšírenie
        public virtual ICollection<PriznakModel> Priznaky { get; set; }
        public virtual ICollection<GenetickeVysetrenieModel> GenetickeVysetrenia { get; set; }

        public PacientModel()
        {
            Diagnozy = new List<DiagnozaModel>();
            Priznaky = new List<PriznakModel>();
            GenetickeVysetrenia = new List<GenetickeVysetrenieModel>();
        }
    }
}
