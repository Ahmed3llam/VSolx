using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VSolx.Models;
using VSolx.View_Model;

namespace VSolx.Controllers
{
    public class CommentController : Controller
    {
        myDbContext dbContext;
        public CommentController(myDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create(int id)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            Detail d = new Detail();

            d.Pid = id;
            d.Product = dbContext.Products.SingleOrDefault(s => s.Id == id);
            //d.Comment = dbContext.Comments.Where(u => u.ProductID == d.Pid).SingleOrDefault();
            return View(d);
        }
        [HttpPost]
        public IActionResult Create(Detail d)
        {
            
            Comment comment = d.Comment;
            comment.UserId = d.Uid;
            comment.ProductID = d.Pid;
            dbContext.Comments.Add(comment);
            dbContext.SaveChanges();
            return RedirectToAction("Detail", "Product", new { id = d.Pid });
        }
    }
}
