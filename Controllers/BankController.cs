using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZivotopisCore.Data;
using ZivotopisCore.Models.BankModels;

namespace ZivotopisCore.Controllers
{
    [Route("[controller]/[action]")]
    public class BankController(AplikaciaDbContext _db) : Controller
    {
        // Prehľad účtov alebo transakcií
        public IActionResult Index()
        {
            var ucty = _db.Ucty
                .Include(u => u.Transakcie)
                .AsNoTracking()
                .ToList();

            return View(ucty);
        }

        // Detail účtu
        public IActionResult Details(int id)
        {
            var ucet = _db.Ucty
                .Include(u => u.Transakcie)
                .AsNoTracking()
                .FirstOrDefault(u => u.Id == id);

            if (ucet is null)
                return NotFound();

            return View(ucet);
        }
        // Vytvorenie nového účtu
        public IActionResult CreateAccount() => View(new UcetModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAccount(UcetModel model)
        {
            if (!ModelState.IsValid) return View(model);

            _db.Ucty.Add(model);
            _db.SaveChanges();
            TempData["SuccessMessage"] = "Účet bol vytvorený.";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult EditAccount(int id)
        {
            var ucet = _db.Ucty.Find(id);
            if (ucet is null) return NotFound();
            return View(ucet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAccount(UcetModel model)
        {
            if (!ModelState.IsValid) return View(model);

            _db.Update(model);
            _db.SaveChanges();
            TempData["SuccessMessage"] = "Účet bol upravený.";
            return RedirectToAction(nameof(Index));
        }


        // Vytvorenie novej transakcie
        public IActionResult Create()
        {
            return View(new TransakciaModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TransakciaModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _db.Transakcie.Add(model);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Transakcia bola úspešne vytvorená.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Chyba pri ukladaní: " + ex.Message;
                return View(model);
            }
        }

        // Úprava transakcie
        public IActionResult Edit(int id)
        {
            var tx = _db.Transakcie.Find(id);
            if (tx is null)
                return NotFound();

            return View(tx);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TransakciaModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Transakcia bola upravená.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Chyba pri ukladaní: " + ex.Message;
                return View(model);
            }
        }

        // Archivácia transakcie
        public IActionResult Archivuj(int id)
        {
            var tx = _db.Transakcie.Find(id);
            if (tx is null)
                return NotFound();

            tx.Archivovana = true;
            _db.SaveChanges();

            TempData["SuccessMessage"] = "Transakcia bola archivovaná.";
            return RedirectToAction(nameof(Index));
        }

        // Zobrazenie archívu
        public IActionResult Archiv()
        {
            var archiv = _db.Transakcie
                .Where(t => t.Archivovana)
                .AsNoTracking()
                .ToList();

            return View(archiv);
        }

        // Obnova transakcie
        public IActionResult Obnov(int id)
        {
            var tx = _db.Transakcie.Find(id);
            if (tx is null)
                return NotFound();

            tx.Archivovana = false;
            _db.SaveChanges();

            TempData["SuccessMessage"] = "Transakcia bola obnovená.";
            return RedirectToAction(nameof(Index));
        }
    }
}
