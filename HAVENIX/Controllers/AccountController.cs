using havenix.Data;
using havenix.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HAVENIX.Controllers
{
    public class AccountController : Controller
    {
        HavenixDbContext db = new HavenixDbContext();

        // ================= REGISTER =================
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User model, string confirmPassword, string BirthDate)
        {
            if (model.Password != confirmPassword)
            {
                ViewBag.Error = "Mật khẩu không khớp";
                return View(model);
            }

            model.BirthDate = DateTime.ParseExact(BirthDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            db.Users.Add(model);
            db.SaveChanges();

            return RedirectToAction("Login");
        }

        // ================= LOGIN =================
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var user = db.Users
                .FirstOrDefault(x => x.Email == email && x.Password == password);

            if (user == null)
            {
                ViewBag.Error = "Sai email hoặc mật khẩu";
                return View();
            }

            Session["User"] = user.Email;
            return RedirectToAction("Index", "Movies");
        }

        // LOGOUT
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}