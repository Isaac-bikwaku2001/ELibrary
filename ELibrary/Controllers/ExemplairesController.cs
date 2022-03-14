using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ELibrary.Data;
using ELibrary.Models;
using Microsoft.AspNetCore.Authorization;

namespace ELibrary.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ExemplairesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExemplairesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Exemplaires
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Exemplaires.Include(e => e.Livre);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Exemplaires/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exemplaire = await _context.Exemplaires
                .Include(e => e.Livre)
                .FirstOrDefaultAsync(m => m.ExemplaireID == id);
            if (exemplaire == null)
            {
                return NotFound();
            }

            return View(exemplaire);
        }

        // GET: Exemplaires/Create
        public IActionResult Create()
        {
            ViewData["LivreID"] = new SelectList(_context.Livres, "LivreID", "Titre");
            return View();
        }

        // POST: Exemplaires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExemplaireID,LivreID,NombreExempalire")] Exemplaire exemplaire)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exemplaire);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LivreID"] = new SelectList(_context.Livres, "LivreID", "Titre", exemplaire.LivreID);
            return View(exemplaire);
        }

        // GET: Exemplaires/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exemplaire = await _context.Exemplaires.FindAsync(id);
            if (exemplaire == null)
            {
                return NotFound();
            }
            ViewData["LivreID"] = new SelectList(_context.Livres, "LivreID", "Titre", exemplaire.LivreID);
            return View(exemplaire);
        }

        // POST: Exemplaires/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExemplaireID,LivreID,NombreExempalire")] Exemplaire exemplaire)
        {
            if (id != exemplaire.ExemplaireID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exemplaire);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExemplaireExists(exemplaire.ExemplaireID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LivreID"] = new SelectList(_context.Livres, "LivreID", "Titre", exemplaire.LivreID);
            return View(exemplaire);
        }

        // GET: Exemplaires/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exemplaire = await _context.Exemplaires
                .Include(e => e.Livre)
                .FirstOrDefaultAsync(m => m.ExemplaireID == id);
            if (exemplaire == null)
            {
                return NotFound();
            }

            return View(exemplaire);
        }

        // POST: Exemplaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exemplaire = await _context.Exemplaires.FindAsync(id);
            _context.Exemplaires.Remove(exemplaire);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExemplaireExists(int id)
        {
            return _context.Exemplaires.Any(e => e.ExemplaireID == id);
        }
    }
}
