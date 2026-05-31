using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HAVENIX.Controllers
{
    public class InfoController : Controller
    {
        public ActionResult About() => View();
        public ActionResult OnlineService() => View();
        public ActionResult GiftCard() => View();
        public ActionResult Recruitment() => View();
        public ActionResult Advertising() => View();
        public ActionResult Partner() => View();

        public ActionResult Terms() => View();
        public ActionResult TransactionTerms() => View();
        public ActionResult PaymentPolicy() => View();
        public ActionResult PrivacyPolicy() => View();
        public ActionResult CinemaRules() => View();
        public ActionResult FAQ() => View();
    }
}