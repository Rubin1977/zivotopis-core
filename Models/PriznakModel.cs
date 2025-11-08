using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZivotopisCore.Models
{
    public class PriznakModel
    {
        public int Id { get; set; }
        public required string Nazov { get; set; }
        public DateTime? DatumZaznamenania { get; set; }

        // Prepojenie na pacienta
        public int PacientModelId { get; set; }
        public required PacientModel Pacient { get; set; }

    }
}