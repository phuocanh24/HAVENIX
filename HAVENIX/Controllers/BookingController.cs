using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using havenix.Data;

namespace HAVENIX.Controllers
{
    public class BookingController : Controller
    {
        private HavenixDbContext db = new HavenixDbContext();

        public ActionResult Create(int showtimeId)
        {
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
            var showtime = db.Showtimes
                .Include("Movie")
                .FirstOrDefault(x => x.Id == showtimeId);

            if (showtime == null)
            {
                return HttpNotFound();
            }

            ViewBag.Seats = seats;
            ViewBag.TicketTotal = total;

            return View(showtime);
        }
    }
}