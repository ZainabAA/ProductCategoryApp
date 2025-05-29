using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductCategoryApp.Data;
using ProductCategoryApp.Models;

namespace ProductCategoryApp.Controllers
{
    public class CategoriesController : Controller
    {
        private AppDbContext db;

        public CategoriesController(AppDbContext _db)
        {
            db = _db;
        }


        public IActionResult Index(string? searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var res = db.Categories.Where(c => c.Name == searchString);
                return View(res);
            }
                return View(db.Categories.AsEnumerable());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category cat)
        {
            if (ModelState.IsValid)
            {
                // Save Data
                db.Categories.Add(cat);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(cat);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var data = db.Categories.Find(id);
            if (data != null)
            {
                return View(data);

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var cat = db.Categories.Find(id);
            if (cat != null)
            {
                return View(cat);

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(Category cat)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Update(cat);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(cat);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var current = db.Categories.Where(e => e.Id == id).FirstOrDefault();
            return View(current);
        }

        [HttpPost]
        public IActionResult Delete(Category cat)
        {
            db.Categories.Remove(cat);
            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Search(string name)
        {
            return RedirectToAction("Index");
        }

    }
}
