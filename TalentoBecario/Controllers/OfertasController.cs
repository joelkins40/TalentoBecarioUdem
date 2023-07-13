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
            string user = Convert.ToString(Session["matricula"]);

            solicitud.alumno = AlumnoService.ConsultarAlumno(user);
            int itemSolicitudExistente = SolicitudesService.ConsultarSiExisteSolicitud(solicitud.alumno.id,solicitud.proyecto.id).Capacity;
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

        [HttpPost]
        public JsonResult ConsultarSolicitud(int id)
        {

            return Json(SolicitudesService.ConsultarSolicitud(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}