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
        var zoznam = await _context.Diagnozy.ToListAsync();
        return View(zoznam);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var diagnoza = await _context.Diagnozy.FindAsync(id);
        return diagnoza is null ? NotFound() : View(diagnoza);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, DiagnozaModel model)
    {
        if (id != model.Id) return BadRequest();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Diagnozy.Any(d => d.Id == model.Id))
                    return NotFound();
                throw;
            }
        }

        return View(model);
    }

    public async Task<IActionResult> Deactivate(int id)
    {
        var dg = await _context.Diagnozy.FindAsync(id);
        if (dg is null) return NotFound();

        dg.Aktivna = false;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
