using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TalentoBecario.Models.Entity;
using TalentoBecario.Models.Services;

namespace TalentoBecario.Controllers
{
    public class InteresesController : Controller
    {
        public ActionResult Index()
        {
            AreaInteres areaInteres = new AreaInteres();


            ViewBag.listAreaIntereses = AreaInteresService.ObtieneListAreaIntereses();

           
            ViewBag.areaInteres = areaInteres;

            return View();
        }

        [HttpPost]
        public JsonResult SaveAreaInteres(AreaInteres areaInteres)
        {
            string message = "";

            if (areaInteres.Id == 0)
            {
                message = AreaInteresService.guardarAreaInteres(areaInteres);
            }
            else
            {
                message = AreaInteresService.ActualizarAreaInteres(areaInteres);
            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ConsultarAreaInteres(int id)
        {
            AreaInteres areaInteres = AreaInteresService.ConsultarAreaInteres(id);
            
            return Json(areaInteres, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EliminarAreaInteres(int id)
        {
            String message = AreaInteresService.EliminarAreaInteres(id);


            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}