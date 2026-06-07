using havenix.Data;
using havenix.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Mvc;

namespace HAVENIX.Controllers
{
    public class AccountController : Controller
    {
        private readonly HavenixDbContext db = new HavenixDbContext();

        // REGISTER
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User model, string password, string confirmPassword, string BirthDate)
        {
            ModelState.Remove("PasswordHash");
            ModelState.Remove("BirthDate");

            if (string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Vui lòng nhập mật khẩu.";
                return View(model);
            }

            if (password != confirmPassword)
            {
                ViewBag.Error = "Mật khẩu không khớp.";
                return View(model);
            }

            if (password.Length < 6)
            {
                ViewBag.Error = "Mật khẩu phải có ít nhất 6 ký tự.";
                return View(model);
            }

            if (!DateTime.TryParseExact(
                BirthDate,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime parsedBirthDate))
            {
                ViewBag.Error = "Ngày sinh không hợp lệ. Vui lòng nhập đúng định dạng dd/MM/yyyy.";
                return View(model);
            }

            model.BirthDate = parsedBirthDate;
            model.Email = (model.Email ?? "").Trim().ToLower();

            if (db.Users.Any(x => x.Email == model.Email))
            {
                ViewBag.Error = "Email này đã được đăng ký.";
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin hợp lệ.";
                return View(model);
            }

            model.PasswordHash = HashPassword(password);

            db.Users.Add(model);
            db.SaveChanges();

            return RedirectToAction("Login");
        }

        // LOGIN
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            email = (email ?? "").Trim().ToLower();

            var user = db.Users.FirstOrDefault(x => x.Email == email);

            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                ViewBag.Error = "Sai email hoặc mật khẩu.";
                return View();
            }

            Session["User"] = user.Email;
            Session["UserId"] = user.Id;
            Session["FullName"] = user.FullName;

            return RedirectToAction("Index", "Movies");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        private static string HashPassword(string password)
        {
            const int iterations = 100000;
            const int saltSize = 16;
            const int keySize = 32;

            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[saltSize];
                rng.GetBytes(salt);

                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
                {
                    byte[] hash = pbkdf2.GetBytes(keySize);

                    return "PBKDF2$" +
                           iterations + "$" +
                           Convert.ToBase64String(salt) + "$" +
                           Convert.ToBase64String(hash);
                }
            }
        }

        private static bool VerifyPassword(string password, string storedHash)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(storedHash))
            {
                return false;
            }

            string[] parts = storedHash.Split('$');

            if (parts.Length != 4 || parts[0] != "PBKDF2")
            {
                return false;
            }

            int iterations = int.Parse(parts[1]);
            byte[] salt = Convert.FromBase64String(parts[2]);
            byte[] expectedHash = Convert.FromBase64String(parts[3]);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                byte[] actualHash = pbkdf2.GetBytes(expectedHash.Length);
                return SlowEquals(actualHash, expectedHash);
            }
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;

            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }

            return diff == 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}