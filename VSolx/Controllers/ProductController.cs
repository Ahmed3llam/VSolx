using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VSolx.Models;
using VSolx.View_Model;
namespace VSolx.Controllers
{
    public class ProductController : Controller
    {
        myDbContext dbContext;
        public ProductController(myDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<Product> p = dbContext.Products.Include(p => p.Category).ToList();
            return View(p);
        }

        public IActionResult Detail(int id)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            Detail d = new Detail();
            d.Pid = id;
            d.Product = dbContext.Products.SingleOrDefault(s => s.Id == id);
            d.comments = dbContext.Comments.Where(u => u.ProductID == id).ToList();
            //d.User = dbContext.Users.Where(u => u.Id == userId).Include(u => u.Products).ThenInclude(p => p.Comments.Where(u => u.ProductID == d.Pid)).SingleOrDefault();
            return View(d);
        }


        public IActionResult Create()
        {
            List<Category> c = dbContext.Categories.ToList();
            SelectList s = new SelectList(c, "Id", "Title");
            ViewBag.c = s;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product, IFormFile Img)
        {
            string path = $"wwwroot/Image/{Img.FileName}";

            FileStream fs = new FileStream(path, FileMode.Create);

            Img.CopyTo(fs);

            product.Img = $"/Image/{Img.FileName}";
            product.UserID = HttpContext.Session.GetInt32("UserId");

            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            //return Content("suc");
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            if (id == null)
            {
                return RedirectToAction("profile");
            }
            Product pro = dbContext.Products.SingleOrDefault(c => c.Id == id);

            if (pro == null)
            {
                return View("Error");
            }
            List<Category> c = dbContext.Categories.ToList();
            SelectList s = new SelectList(c, "Id", "Title");
            ViewBag.c = s;

            Product p = new Product();
            p.Id = pro.Id;
            p.Title = pro.Title;
            p.Body = pro.Body;
            p.Img = pro.Img;
            p.Price = pro.Price;
            p.UserID = pro.UserID;
            p.CategoryId = pro.CategoryId;            
            return View(p);
        }

        [HttpPost]
        public IActionResult Update(Product p)
        {
            // update DB
            Product Old = dbContext.Products.SingleOrDefault(c => c.Id == p.Id);
            if (Old == null)
            {
                return View("Error");
            }
            Old.Id = p.Id;
            Old.Title = p.Title;
            Old.Body = p.Body;
            Old.Price = p.Price;
            Old.CategoryId = p.CategoryId;
            dbContext.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            Product p = dbContext.Products.SingleOrDefault(c => c.Id == id);

            if (p == null)
            {
                return View("bye");
            }

            dbContext.Products.Remove(p);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Cart(int id)
        {
            if (id == null)
            {
                return RedirectToAction("profile");
            }
            Product pro = dbContext.Products.SingleOrDefault(c => c.Id == id);

            if (pro == null)
            {
                return View("Error");
            }
            Cart c = new Cart();
            c.ProductID = pro.Id;
            c.Count = 1;
            c.Tprice = c.Count*pro.Price;
            c.UserID = (int)HttpContext.Session.GetInt32("UserId");
            dbContext.Carts.Add(c);
            dbContext.SaveChanges();
            return RedirectToAction("Index", "Cart");
        }
    }
}
