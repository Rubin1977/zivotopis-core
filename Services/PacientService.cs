using Microsoft.EntityFrameworkCore;
using ZivotopisCore.Data;
using ZivotopisCore.Models;
using ZivotopisCore.Models.ViewModels;

namespace ZivotopisCore.Services;

public class PacientService(AplikaciaDbContext _db)
{
    public void UpravPacienta(EditPacientViewModel model)
    {
        var pacient = _db.Pacienti
            .Include(p => p.Diagnozy)
            .Include(p => p.Priznaky)
            .Include(p => p.GenetickeVysetrenia)
            .FirstOrDefault(p => p.Id == model.Pacient.Id);

        if (pacient is null)
            return;

        AktualizujZakladneUdaje(pacient, model);
        AktualizujDiagnozy(pacient, model);
        AktualizujPriznaky(pacient, model);
        AktualizujVysetrenia(pacient, model);

        _db.SaveChanges();
    }

    private static void AktualizujZakladneUdaje(PacientModel pacient, EditPacientViewModel model)
    {
        pacient.Meno = model.Pacient.Meno;
        pacient.Priezvisko = model.Pacient.Priezvisko;
        pacient.DatumNarodenia = model.Pacient.DatumNarodenia;
        pacient.Pohlavie = model.Pacient.Pohlavie;
        pacient.RodneCislo = model.Pacient.RodneCislo;
        pacient.Archivovany = model.Pacient.Archivovany;
    }

    private void AktualizujDiagnozy(PacientModel pacient, EditPacientViewModel model)
    {
        pacient.Diagnozy.Clear();

        foreach (var idStr in model.SelectedDiagnozy ?? [])
        {
            if (int.TryParse(idStr, out var id))
            {
                var existujuca = _db.Diagnozy.Find(id);
                if (existujuca is not null)
                    pacient.Diagnozy.Add(existujuca);
            }
        }
    }

    private void AktualizujPriznaky(PacientModel pacient, EditPacientViewModel model)
    {
        pacient.Priznaky.Clear();

        foreach (var idStr in model.SelectedPriznaky ?? [])
        {
            if (int.TryParse(idStr, out var id))
            {
                var priznak = _db.Priznaky.Find(id);
                if (priznak is not null)
                    pacient.Priznaky.Add(priznak);
            }
        }
    }

    private void AktualizujVysetrenia(PacientModel pacient, EditPacientViewModel model)
    {
        pacient.GenetickeVysetrenia.Clear();

        foreach (var idStr in model.SelectedVysetrenia ?? [])
        {
            if (int.TryParse(idStr, out var id))
            {
                var vysetrenie = _db.Vysetrenia.Find(id);
                if (vysetrenie is not null)
                    pacient.GenetickeVysetrenia.Add(vysetrenie);
            }
        }
    }
}
