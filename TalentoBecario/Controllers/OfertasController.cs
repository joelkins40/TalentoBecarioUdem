using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TalentoBecario.Models.Entity;
using TalentoBecario.Models.Services;

namespace TalentoBecario.Controllers
{
    public class OfertasController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.listProyectos = ProyectoService.ObtieneListProyectos().Where(o => o.estatus == "En Oferta"); ;
            return View();
        }

        [HttpPost]
        public JsonResult SaveSolicitud(Solicitud solicitud)
        {
            string message = "";
            solicitud.alumno.Pidm = "53";
            int itemSolicitudExistente = SolicitudesService.ConsultarSiExisteSolicitud(Convert.ToInt16(solicitud.alumno.Pidm),solicitud.proyecto.id).Capacity;
            if (itemSolicitudExistente == 0)
            {

           
            if (solicitud.id == 0)
            {
                message = SolicitudesService.guardarSolicitud(solicitud);
            }
            else
            {
                message = SolicitudesService.ActualizarSolicitud(solicitud);
            }
            }
            else
            {
                message = "Ya existe una solicitud pendiente de aprobar, espera a que el formador revise la solicitud.";

            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}