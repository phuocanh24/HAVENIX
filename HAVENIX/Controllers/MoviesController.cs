using havenix.Data;
using havenix.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace havenix.Controllers
{
    public class MoviesController : Controller
    {
        HavenixDbContext db = new HavenixDbContext();

        // MOVIES
        public ActionResult Index()
        {
            var movies = db.Movies.ToList();
            return View(movies);
        }

        // SEARCH
        public ActionResult Search(string keyword)
        {
            var movies = db.Movies
                .Where(x => x.Name.Contains(keyword))
                .GroupBy(x => x.Id)
                .Select(g => g.FirstOrDefault())
                .ToList();

            return View("Index", movies);
        }

        // DETAILS
        public ActionResult Details(int id)
        {
            var movie = db.Movies
                .Include("Showtimes")
                .FirstOrDefault(x => x.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }

        //SHOWTIMES
        public ActionResult Showtimes(int id, DateTime? date)
        {
            var movie = db.Movies.FirstOrDefault(x => x.Id == id);

            if (movie == null)
                return HttpNotFound();

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
    }
}