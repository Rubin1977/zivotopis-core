using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZivotopisCore.Data;
using ZivotopisCore.Models;

namespace ZivotopisCore.Controllers;

[Route("[controller]/[action]")]
public class DiagnozaController(AplikaciaDbContext context) : Controller
{
    private readonly AplikaciaDbContext _context = context;

    public async Task<IActionResult> Index()
    {
        var zoznam = await _context.Diagnozy
            .AsNoTracking()
            .OrderByDescending(d => d.DátumVytvorenia)
            .ToListAsync();

        return View(zoznam);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var diagnoza = await _context.Diagnozy
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);

        return diagnoza is null ? NotFound() : View(diagnoza);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, DiagnozaModel model)
    {
        if (id != model.Id)
            return BadRequest();

        if (!ModelState.IsValid)
            return View(model);

        try
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Diagnóza bola upravená.";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Diagnozy.AnyAsync(d => d.Id == model.Id))
                return NotFound();

            TempData["ErrorMessage"] = "Diagnózu sa nepodarilo uložiť kvôli konfliktu.";
            throw;
        }
    }

    public async Task<IActionResult> Deactivate(int id)
    {
        var dg = await _context.Diagnozy.FindAsync(id);
        if (dg is null)
            return NotFound();

        dg.Aktivna = false;

        try
        {
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Diagnóza bola deaktivovaná.";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Chyba pri deaktivácii: " + ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }
}
