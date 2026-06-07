using havenix.Data;
using havenix.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace HAVENIX.Controllers
{
    public class MoviesController : Controller
    {
        HavenixDbContext db = new HavenixDbContext();

        // MOVIES
        public ActionResult Index()
        {
            var movies = db.Movies
                .Include(x => x.Showtimes)
                .ToList();

            return View(movies);
        }

        // SEARCH
        public ActionResult Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return RedirectToAction("Index");
            }

            keyword = keyword.Trim().ToLower();

            var movie = db.Movies
                .Include(x => x.Showtimes)
                .FirstOrDefault(x =>
                    x.Name.ToLower().Contains(keyword) ||
                    keyword.Contains(x.Name.ToLower())
                );

            if (movie != null)
            {
                return RedirectToAction("Details", "Movies", new { id = movie.Id });
            }

            TempData["Message"] = "Không tìm thấy phim";
            return RedirectToAction("Index");
        }

        // DETAILS
        public ActionResult Details(int? id)
        {
            var movie = db.Movies
                .Include(x => x.Showtimes)
                .FirstOrDefault(x => x.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }

        // SHOWTIMES
        public ActionResult Showtimes(int id, DateTime? date)
        {
            var movie = db.Movies.FirstOrDefault(x => x.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            var selectedDate = date ?? DateTime.Today;

            var showtimes = db.Showtimes
                .Where(x => x.MovieId == id &&
                            x.StartTime.Year == selectedDate.Year &&
                            x.StartTime.Month == selectedDate.Month &&
                            x.StartTime.Day == selectedDate.Day)
                .OrderBy(x => x.StartTime)
                .ToList();

            ViewBag.Movie = movie;
            ViewBag.SelectedDate = selectedDate;

            return View(showtimes);
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