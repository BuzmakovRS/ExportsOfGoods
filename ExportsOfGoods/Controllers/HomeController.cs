using ExportsOfGoods.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExportsOfGoods.Controllers
{

    
    public class HomeController : Controller
    {

        private ExportsContext db = new ExportsContext();
        public ActionResult Index()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                ViewBag.isNotAuth = true;
            }
            return View();
        }

        public ActionResult About()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                ViewBag.isNotAuth = true;
            }
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                ViewBag.isNotAuth = true;
            }
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}