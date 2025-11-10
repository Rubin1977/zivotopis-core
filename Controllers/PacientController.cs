using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZivotopisCore.Data;
using ZivotopisCore.Models;
using ZivotopisCore.Models.ViewModels;
using ZivotopisCore.Services;

namespace ZivotopisCore.Controllers;

public class PacientController(AplikaciaDbContext _db, PacientService _service) : Controller
{
    public IActionResult Index()
    {
        var pacienti = _db.Pacienti
            .Where(p => !p.Archivovany)
            .Include(p => p.Diagnozy)
            .Include(p => p.Priznaky)
            .Include(p => p.GenetickeVysetrenia)
            .ToList();

        return View(pacienti);
    }

    public IActionResult Details(int id)
    {
        var pacient = _db.Pacienti
            .Include(p => p.Priznaky)
            .Include(p => p.GenetickeVysetrenia)
            .Include(p => p.Diagnozy)
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
            Diagnozy = [.. pacient.Diagnozy],
            Priznaky = [.. pacient.Priznaky],
            GenetickeVysetrenia = [.. pacient.GenetickeVysetrenia]
        };

        return View(model);
    }

    public IActionResult Create()
    {
        var model = new PacientFormViewModel
        {
            VsetkyDiagnozy = [.. _db.Diagnozy],
            VsetkyPriznaky = [.. _db.Priznaky],
            VsetkyVysetrenia = [.. _db.Vysetrenia]
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(PacientFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.VsetkyDiagnozy = [.. _db.Diagnozy];
            model.VsetkyPriznaky = [.. _db.Priznaky];
            model.VsetkyVysetrenia = [.. _db.Vysetrenia];
            return View(model);
        }

        var pacient = model.Pacient;

        pacient.Diagnozy = [.. _db.Diagnozy.Where(d => model.SelectedDiagnozy.Contains(d.Id.ToString()))];
        pacient.Priznaky = [.. _db.Priznaky.Where(p => model.SelectedPriznaky.Contains(p.Id.ToString()))];
        pacient.GenetickeVysetrenia = [.. _db.Vysetrenia.Where(v => model.SelectedVysetrenia.Contains(v.Id.ToString()))];


        if (!string.IsNullOrWhiteSpace(model.NovaDiagnoza) &&
            !string.IsNullOrWhiteSpace(model.NovaDiagnozaKod))
        {
            var nova = new DiagnozaModel
            {
                Nazov = model.NovaDiagnoza,
                Kod = model.NovaDiagnozaKod,
                DátumVytvorenia = DateTime.Now,
                Aktivna = true
            };

            _db.Diagnozy.Add(nova);
            pacient.Diagnozy.Add(nova);
        }


        _db.Pacienti.Add(pacient);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var pacient = _db.Pacienti
            .Include(p => p.Diagnozy)
            .Include(p => p.Priznaky)
            .Include(p => p.GenetickeVysetrenia)
            .FirstOrDefault(p => p.Id == id);

        if (pacient is null)
            return NotFound();

        var model = new PacientFormViewModel
        {
            Pacient = pacient,
            VsetkyDiagnozy = [.. _db.Diagnozy],
            VsetkyPriznaky = [.. _db.Priznaky],
            VsetkyVysetrenia = [.. _db.Vysetrenia],
            SelectedDiagnozy = [.. pacient.Diagnozy.Select(d => d.Id.ToString())],
            SelectedPriznaky = [.. pacient.Priznaky.Select(p => p.Id.ToString())],
            SelectedVysetrenia = [.. pacient.GenetickeVysetrenia.Select(v => v.Id.ToString())]


        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(PacientFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.VsetkyDiagnozy = [.. _db.Diagnozy];
            model.VsetkyPriznaky = [.. _db.Priznaky];
            model.VsetkyVysetrenia = [.. _db.Vysetrenia];
            Console.WriteLine("Edit POST triggered");
            return View(model);
        }
        Console.WriteLine("Edit POST triggered");

        _service.UpravPacienta(model);
        return RedirectToAction("Details", new { id = model.Pacient.Id });
    }
    public IActionResult Archivuj(int id)
    {
        var pacient = _db.Pacienti.FirstOrDefault(p => p.Id == id);
        if (pacient == null)
            return NotFound();

        pacient.Archivovany = true;
        _db.SaveChanges();

        return RedirectToAction("Index");
    }
    public IActionResult Archiv()
    {
        var archiv = _db.Pacienti
            .Where(p => p.Archivovany)
            .ToList();

        return View(archiv);
    }
}
