using System.ComponentModel.DataAnnotations;

namespace ZivotopisCore.Models.ViewModels;

public class EditPacientViewModel
{
    public PacientModel Pacient { get; set; } = new PacientModel
    {
        Meno = string.Empty,
        Priezvisko = string.Empty,
        Pohlavie = string.Empty,
        RodneCislo = string.Empty,
        DatumNarodenia = DateTime.Today,
        Archivovany = false
    };

    // Výber existujúcich položiek
    public List<string> SelectedDiagnozy { get; set; } = new();
    public List<string> SelectedPriznaky { get; set; } = new();
    public List<string> SelectedVysetrenia { get; set; } = new();

    // Všetky dostupné možnosti
    public List<DiagnozaModel> VsetkyDiagnozy { get; set; } = new();
    public List<PriznakModel> VsetkyPriznaky { get; set; } = new();
    public List<GenetickeVysetrenieModel> VsetkyVysetrenia { get; set; } = new();
}
