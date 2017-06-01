using ExportsOfGoods.Models;
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
    }
}