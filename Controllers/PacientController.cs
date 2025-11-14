using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZivotopisCore.Data;
using ZivotopisCore.Models;
using ZivotopisCore.Models.ViewModels;

namespace ZivotopisCore.Controllers;

public class PacientController(AplikaciaDbContext _db) : Controller
{
    public IActionResult Index()
    {
        var pacienti = _db.Pacienti
            .Where(p => !p.Archivovany)
            .Include(p => p.Diagnozy)
            .Include(p => p.Priznaky)
            .Include(p => p.GenetickeVysetrenia)
            .AsNoTracking()
            .ToList();

        return View(pacienti);
    }

    public IActionResult Details(int id)
    {
        var pacient = _db.Pacienti
            .Include(p => p.Priznaky)
            .Include(p => p.GenetickeVysetrenia)
            .Include(p => p.Diagnozy)
            .AsNoTracking()
            .FirstOrDefault(p => p.Id == id);

        if (pacient is null)
            return NotFound();

        var model = new PacientDetailViewModel
        {
            Id = pacient.Id,
            Meno = pacient.Meno,
            Priezvisko = pacient.Priezvisko,
            DatumNarodenia = pacient.DatumNarodenia,
            Pohlavie = pacient.Pohlavie,
            RodneCislo = pacient.RodneCislo,
            Diagnozy = pacient.Diagnozy.ToList(),
            Priznaky = pacient.Priznaky.ToList(),
            GenetickeVysetrenia = pacient.GenetickeVysetrenia.ToList()
        };

        return View(model);
    }

    public IActionResult Create()
    {
        var model = new CreatePacientViewModel
        {
            VsetkyDiagnozy = _db.Diagnozy.AsNoTracking().ToList(),
            VsetkyPriznaky = _db.Priznaky.AsNoTracking().ToList(),
            VsetkyVysetrenia = _db.Vysetrenia.AsNoTracking().ToList()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CreatePacientViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.VsetkyDiagnozy = _db.Diagnozy.ToList();
            model.VsetkyPriznaky = _db.Priznaky.ToList();
            model.VsetkyVysetrenia = _db.Vysetrenia.ToList();
            return View(model);
        }

        if (!string.IsNullOrWhiteSpace(model.NovaDiagnoza) &&
            string.IsNullOrWhiteSpace(model.NovaDiagnozaKod))
        {
            ModelState.AddModelError("NovaDiagnozaKod", "Kód diagnózy je povinný, ak zadáte názov.");
            return View(model);
        }

        var pacient = model.Pacient;

        pacient.Diagnozy = _db.Diagnozy
            .Where(d => model.SelectedDiagnozy.Contains(d.Id.ToString()))
            .ToList();

        pacient.Priznaky = _db.Priznaky
            .Where(p => model.SelectedPriznaky.Contains(p.Id.ToString()))
            .ToList();

        pacient.GenetickeVysetrenia = _db.Vysetrenia
            .Where(v => model.SelectedVysetrenia.Contains(v.Id.ToString()))
            .ToList();

        if (!string.IsNullOrWhiteSpace(model.NovaDiagnoza))
        {
            var nova = new DiagnozaModel
            {
                Nazov = model.NovaDiagnoza,
                Kod = model.NovaDiagnozaKod,
                Popis = string.IsNullOrWhiteSpace(model.NovaDiagnozaPopis) ? "Nešpecifikovaný popis" : model.NovaDiagnozaPopis,
                Typ = string.IsNullOrWhiteSpace(model.NovaDiagnozaTyp) ? "Nešpecifikovaný typ" : model.NovaDiagnozaTyp,
                DatumVytvorenia = DateTime.Now,
                Aktivna = true
            };

            _db.Diagnozy.Add(nova);
            pacient.Diagnozy.Add(nova);
        }

        try
        {
            _db.Pacienti.Add(pacient);
            _db.SaveChanges();
            TempData["SuccessMessage"] = "Pacient bol úspešne vytvorený.";
            return RedirectToAction("Details", new { id = pacient.Id });
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Chyba pri ukladaní: " + ex.Message;
            return View(model);
        }
    }

    public IActionResult Edit(int id)
    {
        var pacient = _db.Pacienti
            .Include(p => p.Diagnozy)
            .Include(p => p.Priznaky)
            .Include(p => p.GenetickeVysetrenia)
            .FirstOrDefault(p => p.Id == id);

        if (pacient == null)
            return NotFound();

        var model = new EditPacientViewModel
        {
            Pacient = new PacientModel
            {
                Id = pacient.Id,
                Meno = pacient.Meno,
                Priezvisko = pacient.Priezvisko,
                DatumNarodenia = pacient.DatumNarodenia,
                Pohlavie = pacient.Pohlavie,
                RodneCislo = pacient.RodneCislo,
                Archivovany = pacient.Archivovany
            },
            SelectedDiagnozy = pacient.Diagnozy.Select(d => d.Id.ToString()).ToList(),
            VsetkyDiagnozy = _db.Diagnozy.AsNoTracking().ToList(),

            SelectedPriznaky = pacient.Priznaky.Select(p => p.Id.ToString()).ToList(),
            VsetkyPriznaky = _db.Priznaky.AsNoTracking().ToList(),

            SelectedVysetrenia = pacient.GenetickeVysetrenia.Select(v => v.Id.ToString()).ToList(),
            VsetkyVysetrenia = _db.Vysetrenia.AsNoTracking().ToList()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(EditPacientViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.VsetkyDiagnozy = _db.Diagnozy.ToList();
            model.VsetkyPriznaky = _db.Priznaky.ToList();
            model.VsetkyVysetrenia = _db.Vysetrenia.ToList();
            return View(model);
        }

        var pacient = _db.Pacienti
            .Include(p => p.Diagnozy)
            .Include(p => p.Priznaky)
            .Include(p => p.GenetickeVysetrenia)
            .FirstOrDefault(p => p.Id == model.Pacient.Id);

        if (pacient == null)
            return NotFound();

        pacient.Meno = model.Pacient.Meno;
        pacient.Priezvisko = model.Pacient.Priezvisko;
        pacient.DatumNarodenia = model.Pacient.DatumNarodenia;
        pacient.Pohlavie = model.Pacient.Pohlavie;
        pacient.RodneCislo = model.Pacient.RodneCislo;
        pacient.Archivovany = model.Pacient.Archivovany;

        pacient.Diagnozy = _db.Diagnozy
            .Where(d => model.SelectedDiagnozy.Contains(d.Id.ToString()))
            .ToList();

        pacient.Priznaky = _db.Priznaky
            .Where(p => model.SelectedPriznaky.Contains(p.Id.ToString()))
            .ToList();

        pacient.GenetickeVysetrenia = _db.Vysetrenia
            .Where(v => model.SelectedVysetrenia.Contains(v.Id.ToString()))
            .ToList();

        try
        {
            _db.SaveChanges();
            TempData["SuccessMessage"] = "Zmeny boli úspešne uložené.";
            return RedirectToAction("Details", new { id = pacient.Id });
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Chyba pri ukladaní: " + ex.Message;
            return View(model);
        }
    }

    public IActionResult Archivuj(int id)
    {
        var pacient = _db.Pacienti.Find(id);
        if (pacient == null)
            return NotFound();

        pacient.Archivovany = true;
        _db.SaveChanges();

        TempData["SuccessMessage"] = "Pacient bol archivovaný.";
        return RedirectToAction("Index");
    }

    public IActionResult Archiv()
    {
        var archiv = _db.Pacienti
            .Where(p => p.Archivovany)
            .Include(p => p.Diagnozy)
            .Include(p => p.Priznaky)
            .Include(p => p.GenetickeVysetrenia)
            .AsNoTracking()
            .ToList();

        return View(archiv);
    }

    public IActionResult Obnov(int id)
    {
        var pacient = _db.Pacienti.Find(id);
        if (pacient == null)
            return NotFound();

        pacient.Archivovany = false;

        try
        {
            _db.SaveChanges();
            TempData["SuccessMessage"] = "Pacient bol obnovený.";
            return RedirectToAction("Details", new { id = pacient.Id });
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Chyba pri obnove: " + ex.Message;
            return RedirectToAction("Index");
        }
    }
}