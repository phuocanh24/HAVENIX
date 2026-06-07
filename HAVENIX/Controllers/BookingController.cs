using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using havenix.Data;
using havenix.Filters;

namespace HAVENIX.Controllers
{
    [LoginAuthorize]
    public class BookingController : Controller
    {
        private HavenixDbContext db = new HavenixDbContext();

        private bool IsLoggedIn()
        {
            return Session["UserId"] != null;
        }

        public ActionResult Create(int showtimeId)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            var showtime = db.Showtimes
                .Include(x => x.Movie)
                .FirstOrDefault(x => x.Id == showtimeId);

            if (showtime == null)
            {
                return HttpNotFound();
            }

            return View(showtime);
        }

        public ActionResult Combos(int showtimeId, string seats, int total)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            var showtime = db.Showtimes
                .Include(x => x.Movie)
                .FirstOrDefault(x => x.Id == showtimeId);

            if (showtime == null)
            {
                return HttpNotFound();
            }

            ViewBag.Seats = seats;
            ViewBag.TicketTotal = total;

            return View(showtime);
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