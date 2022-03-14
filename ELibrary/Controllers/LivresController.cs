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
    public class LivresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public LivresController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Livres
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Livres.Include(l => l.Auteur).Include(l => l.Genre);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Livres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livre = await _context.Livres
                .Include(l => l.Auteur)
                .Include(l => l.Genre)
                .FirstOrDefaultAsync(m => m.LivreID == id);
            if (livre == null)
            {
                return NotFound();
            }

            return View(livre);
        }

        // GET: Livres/Create
        public IActionResult Create()
        {
            ViewData["AuteurID"] = new SelectList(_context.Auteurs, "AuteurID", "Nom");
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "Libelle");
            return View();
        }

        // POST: Livres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LivreID,ISBN,Titre,Langue,DateEdition,ImageFile,AuteurID,GenreID")] Livre livre)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(livre.ImageFile.FileName);
            string extension = Path.GetExtension(livre.ImageFile.FileName);
            livre.Image = fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
            string path = Path.Combine(wwwRootPath + "/images/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await livre.ImageFile.CopyToAsync(fileStream);
            }

            _context.Add(livre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            /*
            ViewData["AuteurID"] = new SelectList(_context.Auteurs, "AuteurID", "Nom", livre.AuteurID);
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "Libelle", livre.GenreID);
            return View(livre);
            */
        }

        // GET: Livres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livre = await _context.Livres.FindAsync(id);
            if (livre == null)
            {
                return NotFound();
            }
            ViewData["AuteurID"] = new SelectList(_context.Auteurs, "AuteurID", "Nom", livre.AuteurID);
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "Libelle", livre.GenreID);
            return View(livre);
        }

        // POST: Livres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LivreID,ISBN,Titre,Langue,DateEdition,Image,AuteurID,GenreID")] Livre livre)
        {
            if (id != livre.LivreID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivreExists(livre.LivreID))
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
            ViewData["AuteurID"] = new SelectList(_context.Auteurs, "AuteurID", "Nom", livre.AuteurID);
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "Libelle", livre.GenreID);
            return View(livre);
        }

        // GET: Livres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livre = await _context.Livres
                .Include(l => l.Auteur)
                .Include(l => l.Genre)
                .FirstOrDefaultAsync(m => m.LivreID == id);
            if (livre == null)
            {
                return NotFound();
            }

            return View(livre);
        }

        // POST: Livres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livre = await _context.Livres.FindAsync(id);
            _context.Livres.Remove(livre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivreExists(int id)
        {
            return _context.Livres.Any(e => e.LivreID == id);
        }
    }
}
