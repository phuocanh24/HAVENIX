using System.Linq;
using System.Web.Mvc;
using havenix.Data;
using System.Data.Entity;

namespace HAVENIX.Controllers
{
    public class HomeController : Controller
    {
        private HavenixDbContext db = new HavenixDbContext();

        public ActionResult Index()
        {
            var movies = db.Movies
                .Include(x => x.Showtimes)
                .OrderBy(x => x.Id)
                .ToList();

            return View(movies);
        }

        public ActionResult About() { return View(); }
        public ActionResult Contact() { return View(); }
        public ActionResult Online() { return View(); }
        public ActionResult GiftCard() { return View(); }
        public ActionResult Recruitment() { return View(); }
        public ActionResult Advertising() { return View(); }
        public ActionResult Terms() { return View(); }
        public ActionResult Transaction() { return View(); }
        public ActionResult Payment() { return View(); }
        public ActionResult Privacy() { return View(); }
        public ActionResult Rules() { return View(); }
    }
}