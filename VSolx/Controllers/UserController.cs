using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VSolx.Models;
using VSolx.View_Model;

namespace VSolx.Controllers
{
    public class UserController : Controller
    {
        myDbContext dbContext;
        public UserController(myDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult profile()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }
            User u = dbContext.Users.Where(u => u.Id == userId).Include(u => u.Products).ThenInclude(p => p.Category).SingleOrDefault();
            return View(u);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (user == null)
            {
                return View("myError");
            }
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult RegisterAd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RegisterAd(User user)
        {
            if (user == null)
            {
                return View("myError");
            }
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Login()
        {
            LogVM l = new LogVM();
            return View(l);
        }

        [HttpPost]
        public IActionResult Login(LogVM log)
        {
            User user = dbContext.Users.SingleOrDefault(u => u.Email == log.Email && u.Password == log.Password);
            if (user == null)
            {
                LogVM logvm = new LogVM();
                logvm.Msg = "Wrong Email or Password";
                return View("Login", logvm);
            }
            HttpContext.Session.SetInt32("UserId", user.Id);
            return RedirectToAction("Profile");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        //public IActionResult detail(int id)
        //{
        //    User u = dbContext.Users.SingleOrDefault(s => s.Id == id);
        //    return View(u);
        //}
       
        public IActionResult Update(int id)
        {
            if (id == null)
            {
                return RedirectToAction("profile");
            }
            User empp = dbContext.Users.SingleOrDefault(c => c.Id == id);

            if (empp == null)
            {
                return View("Error");
            }
            User user = new User();
            user.Id = empp.Id;
            user.fname = empp.fname;
            user.lname = empp.lname;
            user.Phone = empp.Phone;
            user.Gender = empp.Gender;
            user.Email = empp.Email;
            user.Password = empp.Password;
            user.Role = empp.Role;
            user.Visa = empp.Visa;
            user.work = empp.work;
            return View(user);
        }

        [HttpPost]
        public IActionResult Update(User user)
        {
            // update DB
            User Olde = dbContext.Users.SingleOrDefault(c => c.Id == user.Id);
            if (Olde == null)
            {
                return View("Error");
            }
            Olde.Id = user.Id;
            Olde.fname = user.fname;
            Olde.lname = user.lname;
            Olde.Phone = user.Phone;
            Olde.Gender = user.Gender;
            Olde.Email = user.Email;
            Olde.Password = user.Password;
            Olde.work = user.work;
            Olde.Role = user.Role;
            Olde.Visa = user.Visa;
            dbContext.SaveChanges();
            return RedirectToAction("profile");
        }
        public IActionResult Delete(int id)
        {
            User user = dbContext.Users.SingleOrDefault(c => c.Id == id);

            if (user == null)
            {
                return View("bye");
            }

            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
            return RedirectToAction("Login");
        }

    }
}
