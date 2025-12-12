namespace ZivotopisCore.Models.ViewModels;

public class PacientFormViewModel
{
    // Hlavný model pacienta
    public PacientModel Pacient { get; set; } = new PacientModel
    {
        Meno = string.Empty,
        Priezvisko = string.Empty,
        Pohlavie = string.Empty,
        RodneCislo = string.Empty,
        DatumNarodenia = DateTime.Today
    };

    public bool Archivovany
    {
        get => Pacient.Archivovany;
        set => Pacient.Archivovany = value;
    }

    // Všetky dostupné možnosti na výber
    public List<DiagnozaModel> VsetkyDiagnozy { get; set; } = [];
    public List<PriznakModel> VsetkyPriznaky { get; set; } = [];
    public List<GenetickeVysetrenieModel> VsetkyVysetrenia { get; set; } = [];

    // Vybrané položky z formulára
    public List<string> SelectedDiagnozy { get; set; } = [];
    public List<string> SelectedPriznaky { get; set; } = [];
    public List<string> SelectedVysetrenia { get; set; } = [];

    // Pridanie novej diagnózy
    public string NovaDiagnoza { get; set; } = string.Empty;
    public string NovaDiagnozaKod { get; set; } = string.Empty;
    public string NovaDiagnozaPopis { get; set; } = string.Empty;
    public string NovaDiagnozaTyp { get; set; } = string.Empty;

    // Pridanie nového príznaku alebo vyšetrenia (voliteľné)
    public string NovaPriznak { get; set; } = string.Empty;
    public string NovaVysetrenie { get; set; } = string.Empty;
}
