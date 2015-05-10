using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Sozial.Controllers

{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //Shows about us
            //User;
            return View();
        }

        public ActionResult Contact()
        {
            //Shows our news

            return View();
        }

        public ActionResult View1()
        {
            ViewBag.Message = "check";
            return View();
        }
    }
}