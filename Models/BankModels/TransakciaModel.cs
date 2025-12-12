using System;

namespace ZivotopisCore.Models.BankModels
{
    public class TransakciaModel
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; } = DateTime.Now;
        public decimal Suma { get; set; }
        public string Protiucet { get; set; } = string.Empty;
        public string Popis { get; set; } = string.Empty;
        public bool Archivovana { get; set; }

        public int UcetId { get; set; }
        public UcetModel? Ucet { get; set; }
    }

}
