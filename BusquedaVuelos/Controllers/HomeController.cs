using BusquedaVuelos.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusquedaVuelos.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Descripción de su aplicación.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Tu página de contacto.";

            return View();
        }
    }
}