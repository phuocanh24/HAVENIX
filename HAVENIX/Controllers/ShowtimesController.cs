using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using havenix.Data;

namespace HAVENIX.Controllers
{
    public class ShowtimesController : Controller
    {
        private HavenixDbContext db = new HavenixDbContext();

        public ActionResult Index()
        {
            var showtimes = db.Showtimes
                .Include(s => s.Movie)
                .OrderBy(s => s.StartTime)
                .ToList();

            return View(showtimes);
        }
    }
}