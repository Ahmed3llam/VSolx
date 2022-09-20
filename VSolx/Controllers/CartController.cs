using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VSolx.Models;
namespace VSolx.Controllers
{
    public class CartController : Controller
    {
        myDbContext dbContext;
        public CartController(myDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }
            User u = dbContext.Users.Where(u => u.Id == userId).Include(u => u.cart)
                .ThenInclude(p => p.product).SingleOrDefault();
            return View(u);
        }
        public IActionResult Update(int id)
        {
            if (id == null)
            {
                return RedirectToAction("profile");
            }
            Cart c = dbContext.Carts.SingleOrDefault(c => c.Id == id);

            if (c == null)
            {
                return View("Error");
            }
            Cart Cart = new Cart();
            Cart.Id = c.Id;
            Cart.ProductID = c.ProductID;
            Cart.UserID = c.UserID;
            Cart.Count = c.Count;
            Cart.Tprice = c.Tprice;
            return View(Cart);
        }

        [HttpPost]
        public IActionResult Update(Cart Cart , Product product)
        {
            // update DB
            Cart Old = dbContext.Carts.SingleOrDefault(c => c.Id == Cart.Id);
            Product Oldc = dbContext.Products.SingleOrDefault(c => c.Id == Cart.ProductID);
            if (Old == null)
            {
                return View("Error");
            }
            Old.Id = Cart.Id;
            Old.Count = Cart.Count;
            //product.Price  = Cart.Tprice;
            Cart.Tprice= product.Price * Cart.Count;
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Cart ca = dbContext.Carts.SingleOrDefault(c => c.Id == id);
            if (ca == null)
            {
                return View("Error");
            }
            dbContext.Carts.Remove(ca);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
