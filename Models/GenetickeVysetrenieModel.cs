using System;
using System.Collections.Generic;
using System.Linq;


namespace ZivotopisCore.Models
{
    public class GenetickeVysetrenieModel
    {
        public int Id { get; set; }
        public required string Nazov { get; set; }
        public required string Vysledok { get; set; }
        public DateTime? DatumVysetrenia { get; set; }

        // Prepojenie na pacienta
        public int PacientModelId { get; set; }
        public required PacientModel Pacient { get; set; }
    }
}