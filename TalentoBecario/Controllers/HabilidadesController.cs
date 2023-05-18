using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TalentoBecario.Models.Entity;
using TalentoBecario.Models.Services;

namespace TalentoBecario.Controllers
{
    public class HabilidadesController : Controller
    {
        public ActionResult Index()
        {
            Habilidad habilidad = new Habilidad();


          
            ViewBag.listHabilidades = HabilidadesService.ObtieneListHabilidades();

            
            ViewBag.habilidad = habilidad;

            return View();
        }

        [HttpPost]
        public JsonResult SaveHabilidad(Habilidad habilidad)
        {
            string message = "";

            if (habilidad.Id == 0)
            {
                message= HabilidadesService.guardarHabilidad(habilidad);
            }
            else
            {
                message= HabilidadesService.ActualizarHabilidad(habilidad);
            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ConsultarHabilidad(int id)
        {
            Habilidad habilidad= HabilidadesService.ConsultarHabilidad(id);

            
            return Json(habilidad, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarHabilidad(int id)
        {
            String message = HabilidadesService.EliminarHabilidad(id);


            return Json("Registro Eliminado", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}