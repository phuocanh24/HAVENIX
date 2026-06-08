using havenix.Data;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace HAVENIX.Controllers
{
    public class ShowtimesController : Controller
    {
        private HavenixDbContext db = new HavenixDbContext();

        public ActionResult Index()
        {
            var now = DateTime.Now;

            var showtimes = db.Showtimes
                .Include("Movie")
                .Where(x => x.StartTime >= now)
                .OrderBy(x => x.StartTime)
                .ToList();

            return View(showtimes);
        }
    }
}