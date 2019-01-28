using MadeWithUnityRandomShowcase.Models;
using MadeWithUnityRandomShowcase.Repository;
using System;
using System.Web.Mvc;

namespace MadeWithUnityRandomShowcase.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Showcase showcase = ShowcaseRepo.GetShowcaseInfo();
            RanGenNum rando = new RanGenNum();
            return View(showcase);
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
    }
}