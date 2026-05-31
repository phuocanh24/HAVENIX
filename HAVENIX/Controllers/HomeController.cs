using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HAVENIX.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Online()
        {
            return View();
        }

        public ActionResult GiftCard()
        {
            return View();
        }

        public ActionResult Recruitment()
        {
            return View();
        }

        public ActionResult Advertising()
        {
            return View();
        }

        public ActionResult Terms()
        {
            return View();
        }

        public ActionResult Transaction()
        {
            return View();
        }

        public ActionResult Payment()
        {
            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }

        public ActionResult Rules()
        {
            return View();
        }
    }
}