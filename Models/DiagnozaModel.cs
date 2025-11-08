using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZivotopisCore.Models
{
    public class DiagnozaModel
    {
        public int Id { get; set; }
        public required string Nazov { get; set; }
        public string? Popis { get; set; }

        // Navigačná vlastnosť pre Many-to-Many
        public virtual ICollection<PacientModel> Pacienti { get; set; }
        public DiagnozaModel()
        {
            Pacienti = new List<PacientModel>();
        }
    }
}