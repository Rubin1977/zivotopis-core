using System.Collections.Generic;

namespace ZivotopisCore.Models.BankModels
{
    public class UcetModel
    {
        public int Id { get; set; }
        public string CisloUctu { get; set; } = string.Empty;
        public string Majitel { get; set; } = string.Empty;
        public decimal Zostatok { get; set; }

        public List<TransakciaModel> Transakcie { get; set; } = new();
    }

}

