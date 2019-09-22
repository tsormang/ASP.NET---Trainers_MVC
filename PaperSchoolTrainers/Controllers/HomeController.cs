using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaperSchoolTrainers.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Paper Rules.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Yep! My Contact Page ... that is.";

            return View();
        }
    }
}