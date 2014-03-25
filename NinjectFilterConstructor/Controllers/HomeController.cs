using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NinjectFilterConstructor.Filters;
using NinjectFilterConstructor.Interface;
using NinjectFilterConstructor.POCO;

namespace NinjectFilterConstructor.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPeople peps;

        public HomeController(IPeople people)
        {
            peps = people;
        }

        [TritonAction(TassAppActions.ViewContests, true)]
        public ActionResult Index()
        {
            ViewBag.Message = peps.Name();
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
