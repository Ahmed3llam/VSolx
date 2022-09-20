using Microsoft.AspNetCore.Mvc;
using VSolx.Models;

namespace VSolx.Controllers
{
    public class CategoryController : Controller
    {
        myDbContext dbContext;
        public CategoryController(myDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            List<Category> c = dbContext.Categories.ToList();
            return View(c);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category Category)
        {
            dbContext.Categories.Add(Category);
            dbContext.SaveChanges();
            return RedirectToAction("Create");
        }
        public IActionResult Update(int id)
        {
            if (id == null)
            {
                return RedirectToAction("profile");
            }
            Category c = dbContext.Categories.SingleOrDefault(c => c.Id == id);

            if (c == null)
            {
                return View("Error");
            }
            Category cat = new Category();
            cat.Id = c.Id;
            cat.Title = c.Title;
            return View(cat);
        }

        [HttpPost]
        public IActionResult Update(Category cat)
        {
            // update DB
            Category Old = dbContext.Categories.SingleOrDefault(c => c.Id == cat.Id);
            if (Old == null)
            {
                return View("Error");
            }
            Old.Id = cat.Id;
            Old.Title = cat.Title;
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Category c = dbContext.Categories.SingleOrDefault(c => c.Id == id);

            if (c == null)
            {
                return View("bye");
            }

            dbContext.Categories.Remove(c);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
