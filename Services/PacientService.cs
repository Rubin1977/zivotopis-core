using ZivotopisCore.Data;
using ZivotopisCore.Models;
using ZivotopisCore.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ZivotopisCore.Services
{
    public class PacientService(AplikaciaDbContext _db)
    {
        public void UpravPacienta(PacientFormViewModel model)
        {
            var pacient = _db.Pacienti
                .Include(p => p.Diagnozy)
                .Include(p => p.Priznaky)
                .Include(p => p.GenetickeVysetrenia)
                .FirstOrDefault(p => p.Id == model.Pacient.Id);

            Console.WriteLine("UpravPacienta called");

            if (pacient is null)
                return;

            // ✅ Aktualizácia základných údajov
            pacient.Meno = model.Pacient.Meno;
            pacient.Priezvisko = model.Pacient.Priezvisko;
            pacient.DatumNarodenia = model.Pacient.DatumNarodenia;
            pacient.Pohlavie = model.Pacient.Pohlavie;
            pacient.RodneCislo = model.Pacient.RodneCislo;

            // ✅ Diagnózy (podľa názvu, nie ID)
            pacient.Diagnozy.Clear();
            foreach (var nazov in model.SelectedDiagnozy)
            {
                if (string.IsNullOrWhiteSpace(nazov)) continue;

                var existujuca = _db.Diagnozy.FirstOrDefault(d => d.Nazov == nazov);
                if (existujuca != null)
                {
                    pacient.Diagnozy.Add(existujuca);
                }
                else
                {
                    var nova = new DiagnozaModel { Nazov = nazov };
                    _db.Diagnozy.Add(nova);
                    pacient.Diagnozy.Add(nova);
                }
            }

            // ✅ Príznaky (podľa názvu)
            pacient.Priznaky.Clear();
            foreach (var nazov in model.SelectedPriznaky)
            {
                if (string.IsNullOrWhiteSpace(nazov)) continue;

                var existujuci = _db.Priznaky.FirstOrDefault(p => p.Nazov == nazov);
                if (existujuci != null)
                {
                    pacient.Priznaky.Add(existujuci);
                }
                else
                {
                    var novy = new PriznakModel
                    {
                        Nazov = nazov,
                        DatumZaznamenania = DateTime.Now,
                        Pacient = pacient
                    };
                    _db.Priznaky.Add(novy);
                    pacient.Priznaky.Add(novy);
                }
            }

            // ✅ Genetické vyšetrenia (podľa názvu)
            pacient.GenetickeVysetrenia.Clear();
            foreach (var nazov in model.SelectedVysetrenia)
            {
                if (string.IsNullOrWhiteSpace(nazov)) continue;

                var existujuce = _db.Vysetrenia.FirstOrDefault(v => v.Nazov == nazov);
                if (existujuce != null)
                {
                    pacient.GenetickeVysetrenia.Add(existujuce);
                }
                else
                {
                    var nove = new GenetickeVysetrenieModel
                    {
                        Nazov = nazov,
                        DatumVysetrenia = DateTime.Now,
                        Vysledok = string.Empty,
                        Pacient = pacient
                    };
                    _db.Vysetrenia.Add(nove);
                    pacient.GenetickeVysetrenia.Add(nove);
                }
            }

            Console.WriteLine("Calling SaveChanges...");
            _db.SaveChanges();
        }
    }
}
