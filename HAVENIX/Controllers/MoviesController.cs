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
        public ActionResult Index(string genre, string status)
        {
            var movies = db.Movies
                .Include(x => x.Showtimes)
                .AsQueryable();

            if (!string.IsNullOrEmpty(genre))
            {
                movies = movies.Where(x => x.Genre == genre);
            }

            if (!string.IsNullOrEmpty(status))
            {
                movies = movies.Where(x => x.Status == status);
            }

            ViewBag.SelectedGenre = genre;
            ViewBag.SelectedStatus = status;

            ViewBag.Genres = db.Movies
                .Select(x => x.Genre)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            ViewBag.Statuses = db.Movies
                .Select(x => x.Status)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            return View(movies.ToList());
        }

        // SEARCH
        public ActionResult Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return RedirectToAction("Index");
            }

            keyword = keyword.Trim().ToLower();

            var movies = db.Movies
                .Include(x => x.Showtimes)
                .Where(x =>
                    x.Name.ToLower().Contains(keyword) ||
                    x.Genre.ToLower().Contains(keyword) ||
                    x.Description.ToLower().Contains(keyword) ||
                    x.Language.ToLower().Contains(keyword) ||
                    x.Status.ToLower().Contains(keyword)
                )
                .OrderBy(x => x.Name)
                .ToList();

            ViewBag.Keyword = keyword;

            return View("Index", movies);
        }

        // DETAILS
        public ActionResult Details(int? id)
        {
            var now = DateTime.Now;

            var movie = db.Movies
                .Include(x => x.Showtimes)
                .FirstOrDefault(x => x.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            movie.Showtimes = movie.Showtimes
                .Where(x => x.StartTime >= now)
                .OrderBy(x => x.StartTime)
                .ToList();

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