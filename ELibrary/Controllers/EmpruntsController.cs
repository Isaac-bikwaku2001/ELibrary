using ELibrary.Data;
using ELibrary.Helpers;
using ELibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ELibrary.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmpruntsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EmpruntsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Emp()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Livre>>(HttpContext.Session, "cart");
            Emprunt emprunt = new Emprunt();

            foreach (var item in cart)
            {
                var livre = _context.Livres.Find(item.LivreID);
                var user = _userManager.GetUserId(HttpContext.User);

                emprunt.LivreID = livre.LivreID;
                emprunt.UserId = user;
                emprunt.DateEmprunt = DateTime.Now;

                _context.Emprunts.Add(emprunt);
                _context.SaveChanges();
            }

            cart.RemoveRange(0, cart.Count);
            return RedirectToAction("Index", "Home");
        }
    }
}
