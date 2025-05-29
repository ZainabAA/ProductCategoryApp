using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductCategoryApp.Data;
using ProductCategoryApp.Models;

namespace ProductCategoryApp.Controllers
{
    public class ProductsController : Controller
    {
        private AppDbContext db;

        public ProductsController(AppDbContext _db)
        {
            db = _db;
        }


        public IActionResult Index(string? searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var res = db.Products.Include(c => c.Category).Where(c => c.Name == searchString);
                return View(res);
            }
            return View(db.Products.Include(c => c.Category).AsEnumerable());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AllCats = new SelectList(db.Categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product prod)
        {
            ViewBag.AllCats = new SelectList(db.Categories, "Id", "Name");
            if (ModelState.IsValid)
            {
                // Save Data
                db.Products.Add(prod);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(prod);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var data = db.Products.Include(c => c.Category).Where(p=>p.Id == id).FirstOrDefault();
            if (data != null)
            {
                return View(data);

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.AllCats = new SelectList(db.Categories, "Id", "Name");
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var prod = db.Products.Find(id);
            if (prod != null)
            {
                return View(prod);

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(Product prod)
        {
            ViewBag.AllCats = new SelectList(db.Categories, "Id", "Name");
            if (ModelState.IsValid)
            {
                db.Products.Update(prod);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(prod);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var current = db.Products.Where(e => e.Id == id).FirstOrDefault();
            return View(current);
        }

        [HttpPost]
        public IActionResult Delete(Product prod)
        {
            db.Products.Remove(prod);
            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
