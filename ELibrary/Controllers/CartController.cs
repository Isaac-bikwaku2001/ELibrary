using ELibrary.Data;
using ELibrary.Helpers;
using ELibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace ELibrary.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Livre>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.Message = "votre panier est vide";
            //ViewBag.UserID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View();
        }

        public IActionResult Emprunter(int? id)
        {
            var livre = _context.Livres.Find(id);

            if (SessionHelper.GetObjectFromJson<List<Livre>>(HttpContext.Session, "cart") == null)
            {
                var cart = new List<Livre>();

                cart.Add(new Livre() { 
                    LivreID = livre.LivreID,
                    ISBN = livre.ISBN,
                    Titre = livre.Titre,
                    Langue = livre.Langue,
                    DateEdition = livre.DateEdition,
                    Image = livre.Image,
                    AuteurID = livre.AuteurID,
                    GenreID = livre.GenreID
                });

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                var cart = SessionHelper.GetObjectFromJson<List<Livre>>(HttpContext.Session, "cart");
                var index = Exists(cart, id);
                if (index == -1)
                {
                    cart.Add(new Livre()
                    {
                        LivreID = livre.LivreID,
                        ISBN = livre.ISBN,
                        Titre = livre.Titre,
                        Langue = livre.Langue,
                        DateEdition = livre.DateEdition,
                        Image = livre.Image,
                        AuteurID = livre.AuteurID,
                        GenreID = livre.GenreID
                    });
                }

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Remove(int? id)
        {
            // var livre = _context.Livres.Find(id);

            var cart = SessionHelper.GetObjectFromJson<List<Livre>>(HttpContext.Session, "cart");
            var index = Exists(cart, id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            
            return RedirectToAction("Index");
        }

        private int Exists(List<Livre> cart, int? id)
        {
            for(int i = 0; i < cart.Count; i++)
            {
                if(cart[i].LivreID == id)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
