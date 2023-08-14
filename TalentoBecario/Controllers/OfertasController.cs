using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TalentoBecario.Models.Entity;
using TalentoBecario.Models.Services;

namespace TalentoBecario.Controllers
{
    [Authorize]
    public class OfertasController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.NameUser = Convert.ToString(Session["nombreUser"]);
            ViewBag.listProyectos = ProyectoService.ObtieneListProyectos().Where(o => o.estatus == "En Oferta");
            return View();
        }

        [HttpPost]
        public JsonResult SaveSolicitud(Solicitud solicitud)
        {
            string message = "";
            string user = Convert.ToString(Session["matricula"]);

            solicitud.alumno = AlumnoService.ConsultarAlumno(user);
            int itemSolicitudExistente = SolicitudesService.ConsultarSiExisteSolicitud(solicitud.alumno.pidm, solicitud.proyecto.id).Capacity;
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
        [HttpPost]
        public JsonResult ActualizarSolicitud(int id, string estatus)
        {
            Solicitud itemSolicitud = SolicitudesService.ConsultarSolicitud(id);
                itemSolicitud.estatus = estatus;
            if (estatus.Equals("Asignado"))
            {
                itemSolicitud.alumno.formador = itemSolicitud.proyecto.formador;
                AlumnoService.ActualizarAlumno(itemSolicitud.alumno);
                
            }
            return Json(SolicitudesService.ActualizarSolicitud(itemSolicitud), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult rechazarSolicitud(int id)
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