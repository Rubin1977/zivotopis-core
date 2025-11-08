namespace ZivotopisCore.Models.ViewModels
{
    public class PacientEditViewModel
    {
        public int Id { get; set; }
        public string Meno { get; set; } = string.Empty;
        public string Priezvisko { get; set; } = string.Empty;
        public DateTime DatumNarodenia { get; set; }
        public string Pohlavie { get; set; } = string.Empty;
        public string RodneCislo { get; set; } = string.Empty;

        public bool Archivovany { get; set; }

        public List<int> VybraneDiagnozyIds { get; set; } = [];
        public List<DiagnozaModel> VsetkyDiagnozy { get; set; } = [];
    }
}
